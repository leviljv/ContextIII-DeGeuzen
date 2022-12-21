using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : MoveState {
    Vector3 currentDir;

    public SlidingState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.animator.SetBool("Sliding", true);

        owner.controller.height = 1;
        owner.controller.center = new Vector3(0, .5f, 0);

        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentDir = owner.SlopeTransform.TransformDirection(input.normalized);
    }

    public override void OnExit() {
        owner.animator.SetBool("Sliding", false);

        owner.controller.height = 2;
        owner.controller.center = new Vector3(0, 1, 0);
    }

    public override void OnUpdate() {
        Vector3 velocity;

        //move
        Vector3 downDir = Vector3.Cross(Vector3.Cross(Vector3.up, owner.evaluator.GetSlopeNormal()), owner.evaluator.GetSlopeNormal());

        currentDir = Vector3.Lerp(currentDir, downDir, 1f * Time.deltaTime);

        velocity = currentDir * owner.slideSpeed;

        owner.velocity = velocity;

        //jump 
        if (Input.GetKeyDown(KeyCode.Space)) {
            owner.animator.SetBool("Sliding", false);
            owner.animator.SetTrigger("Jump");
            owner.velocity += new Vector3(0, Mathf.Sqrt(owner.jumpHeight * -2 * owner.gravity), 0);
        }

        base.OnUpdate();
    }
}