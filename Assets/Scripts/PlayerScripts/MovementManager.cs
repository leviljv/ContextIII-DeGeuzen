using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementManager : MonoBehaviour
{
    [Header("Open Vars")]
    public Vector3 velocity;

    //StateMachine
    private StateMachine<MovementManager> movementStateMachine;

    [Header("Objects Needed")]
    public GameObject Visuals;
    public GameObject GroundCheck;
    public GameObject LedgeCheck;
    public Transform SlopeTransform;
    public Transform YRotationParent;
    public LayerMask GroundLayer;
    public LayerMask EdgeLayer;
    public GameObject DebugObject;

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public MovementEvaluator evaluator;

    [Header("World Settings")]
    public float gravity = -19.62f;
    public float airDrag = 10;
    public float Acceleration;

    [Header("Player Settings")]
    public float maxSpeed;
    public float speed;
    public float airbornSpeed;
    public float runSpeed;
    public float slideSpeed;
    public float crouchSpeed;
    public float jumpHeight;
    public int jumpAmount;
    public float spherecheckRadius;
    public float climbTime;
    public float collectableRadius;

    //Keep Track of Info
    [HideInInspector] public Vector3 CurrentDirection; 
    [HideInInspector] public Vector3 GroundAngle;
    [HideInInspector] public bool sprinting;
    [HideInInspector] public bool canGrabNextLedge = true;
    [HideInInspector] public bool lookAtMoveDir = true;
    [HideInInspector] public GameObject CurrentLedge = null;

    [HideInInspector] public bool Interacting;

    private void OnEnable() {
        EventManager<bool>.Subscribe(EventType.SET_INTERACTION_STATE, SetInteracting);
    }
    private void OnDisable() {
        EventManager<bool>.Unsubscribe(EventType.SET_INTERACTION_STATE, SetInteracting);
    }

    void Start() {
        controller = GetComponent<CharacterController>();

        canGrabNextLedge = true;

        movementStateMachine = new(this);

        evaluator = new();
        evaluator.owner = this;

        var groundedState = new GroundedState(movementStateMachine);
        movementStateMachine.AddState(typeof(GroundedState), groundedState);
        AddTransitionWithKey(groundedState, KeyCode.Space, typeof(AirbornState));
        AddTransitionWithPrediquete(groundedState, (x) => { return !evaluator.IsGrounded(); }, typeof(AirbornState));
        AddTransitionWithBool(groundedState, !evaluator.IsGrounded(), typeof(AirbornState));
        AddTransitionWithPrediquete(groundedState, (x) => {
            var tmp = evaluator.CollectableNearby();
            if (tmp != null) {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    tmp.GetComponent<IInteractable>().Interact();
                    return false;
                }
            }
            return false;
        }, typeof(InteractionState));
        AddTransitionWithPrediquete(groundedState, (x) => { return Interacting; }, typeof(InteractionState));

        var airbornState = new AirbornState(movementStateMachine);
        movementStateMachine.AddState(typeof(AirbornState), airbornState);
        AddTransitionWithPrediquete(airbornState, (x) => { return evaluator.IsGrounded() && !sprinting; }, typeof(GroundedState));
        AddTransitionWithPrediquete(airbornState, (x) => {
            var tmp = evaluator.SphereCast(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0), 0, spherecheckRadius);
            if (tmp != null && canGrabNextLedge) {
                CurrentLedge = tmp;
                return true;
            }
            return false;
        }, typeof(GrabNextLedgeState));

        var wallClimbState = new GrabNextLedgeState(movementStateMachine);
        movementStateMachine.AddState(typeof(GrabNextLedgeState), wallClimbState);
        AddTransitionWithPrediquete(wallClimbState, (x) => { return wallClimbState.isDone; }, typeof(LedgeGrabbingState));

        var goOntoPlatformState = new GetUpOnPlatformState(movementStateMachine);
        movementStateMachine.AddState(typeof(GetUpOnPlatformState), goOntoPlatformState);
        AddTransitionWithPrediquete(goOntoPlatformState, (x) => { return goOntoPlatformState.IsDone; }, typeof(AirbornState));

        var interactionState = new InteractionState(movementStateMachine);
        movementStateMachine.AddState(typeof(InteractionState), interactionState);
        AddTransitionWithPrediquete(interactionState, (x) => { return !Interacting; }, typeof(GroundedState));

        movementStateMachine.ChangeState(typeof(GroundedState));
    }

    void Update() {
        movementStateMachine.OnUpdate();

        SlopeTransform.rotation = Quaternion.FromToRotation(SlopeTransform.up, evaluator.GetSlopeNormal()) * SlopeTransform.rotation;
        SlopeTransform.localEulerAngles = new Vector3(SlopeTransform.localEulerAngles.x, 0, SlopeTransform.localEulerAngles.z);

        controller.Move(velocity * Time.deltaTime);

        if(velocity.magnitude > 1 && lookAtMoveDir) {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity.normalized, 30 * Time.deltaTime, 0f);
            desiredForward.y = 0;
            transform.LookAt(transform.position + desiredForward);
        }
    }

    public void AddTransitionWithKey(State<MovementManager> state, KeyCode keyCode, System.Type stateTo) {
        state.AddTransition(new Transition<MovementManager>(
            (x) => {
                if (Input.GetKeyDown(keyCode)) {
                    return true;
                }
                return false;
            }, stateTo));
    }

    public void AddTransitionWithBool(State<MovementManager> state, bool check, System.Type stateTo) {
        state.AddTransition(new Transition<MovementManager>(
            (x) => {
                if (check)
                    return true;
                return false;
            }, stateTo));
    }

    public void AddTransitionWithPrediquete(State<MovementManager> state, System.Predicate<MovementManager> predicate, System.Type stateTo) {
        state.AddTransition(new Transition<MovementManager>(predicate, stateTo));
    }

    public void ResetTimer(float time) => StartCoroutine(Timer(time));

    public void SetInteracting(bool setting) {
        Interacting = setting;
    }

    public IEnumerator Timer(float time) {
        yield return new WaitForSeconds(time);
        canGrabNextLedge = true;
    }
}