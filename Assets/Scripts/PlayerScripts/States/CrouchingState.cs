using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MoveState
{
    public CrouchingState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.controller.height = 1;
        owner.controller.center = new Vector3(0, .5f, 0);
        owner.CameraFollow.transform.localPosition = new Vector3(0, 1.3f, 0);
    }

    public override void OnExit() {
        owner.controller.height = 2;
        owner.controller.center = new Vector3(0, 1f, 0);
        owner.CameraFollow.transform.localPosition = new Vector3(0, 1.8f, 0);
    }

    public override void OnUpdate() {
        Vector3 velocity = Vector3.zero;

        //inputs
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //move
        var movedir = owner.SlopeTransform.TransformDirection(input.normalized);
        velocity += movedir * owner.crouchSpeed;

        //jump 
        if (Input.GetKeyDown(KeyCode.Space)) {
            owner.velocity += new Vector3(0, Mathf.Sqrt(owner.jumpHeight * -2 * owner.gravity), 0);
        }

        owner.velocity = Vector3.MoveTowards(owner.velocity, velocity, owner.Acceleration * Time.deltaTime);

        base.OnUpdate();
    }
}