using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingState : MoveState {

    public SprintingState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.animator.SetBool("Sprinting", true);

        owner.sprinting = true;
    }

    public override void OnExit() {
        owner.animator.SetBool("Sprinting", false);
    }

    public override void OnUpdate() {
        Vector3 velocity = Vector3.zero;

        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //move
        var movedir = owner.SlopeTransform.TransformDirection(input.normalized);
        velocity += movedir * owner.runSpeed;

        if (input.magnitude == 0)
            owner.animator.SetBool("Walking", false);

        //jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            owner.animator.SetTrigger("Jump");
            owner.velocity += new Vector3(0, Mathf.Sqrt(owner.jumpHeight * -2 * owner.gravity), 0);
        }

        owner.velocity = Vector3.MoveTowards(owner.velocity, velocity, owner.Acceleration * Time.deltaTime);

        base.OnUpdate();
    }
}