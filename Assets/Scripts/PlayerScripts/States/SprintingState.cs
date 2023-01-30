using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingState : MoveState {
    private bool isWalking = false;
    private bool PreviousisWalking = false;

    public SprintingState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.sprinting = true;

        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input.magnitude! > 0) {
            isWalking = true;
        }
    }

    public override void OnExit() {
        owner.audioManager.PlayLoopedAudio("Walking", false);
        isWalking = false;
        PreviousisWalking = false;
    }

    public override void OnUpdate() {
        Vector3 velocity = Vector3.zero;

        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //move
        var movedir = owner.SlopeTransform.TransformDirection(input.normalized);
        velocity += movedir * owner.runSpeed;

        if (input.normalized.magnitude > 0)
            isWalking = true;
        else
            isWalking = false;

        if (isWalking != PreviousisWalking) {
            owner.audioManager.PlayLoopedAudio("Walking", isWalking);
        }
        PreviousisWalking = isWalking;

        //jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            owner.velocity += new Vector3(0, Mathf.Sqrt(owner.jumpHeight * -2 * owner.gravity), 0);
        }

        owner.velocity = Vector3.MoveTowards(owner.velocity, velocity, owner.Acceleration * Time.deltaTime);

        base.OnUpdate();
    }
}