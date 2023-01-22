using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : MoveState {
    private float previousValue;
    private bool Toggle = true;

    public GroundedState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.lookAtMoveDir = true;

        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input.magnitude! > 0) {
            previousValue = input.normalized.magnitude;
            Toggle = true;
        }

        owner.audioManager.PlayLoopedAudio("Walking", Toggle);
    }

    public override void OnExit() {
        owner.audioManager.PlayLoopedAudio("Walking", false);
        Toggle = false;
    }

    public override void OnUpdate() {
        Vector3 velocity = Vector3.zero;

        //inputs
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //move
        var movedir = owner.SlopeTransform.TransformDirection(input.normalized);
        velocity += movedir * owner.speed;

        if (input.normalized.magnitude != previousValue) {
            owner.audioManager.PlayLoopedAudio("Walking", Toggle);
            Toggle = !Toggle;
        }
        previousValue = input.normalized.magnitude;

        //jump 
        if (Input.GetKeyDown(KeyCode.Space)) {
            owner.audioManager.PlayLoopedAudio("Walking", false);
            owner.audioManager.PlayAudio("Jump");
            owner.velocity += new Vector3(0, Mathf.Sqrt(owner.jumpHeight * -2 * owner.gravity), 0);
        }

        owner.velocity = Vector3.MoveTowards(owner.velocity, velocity, owner.Acceleration * Time.deltaTime);
        owner.LastGroundedPos = owner.transform.position + new Vector3(0, .2f, 0);

        base.OnUpdate();
    }
}