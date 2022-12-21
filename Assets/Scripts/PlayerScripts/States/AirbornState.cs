using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : MoveState {

    private int doubleJumps;

    public AirbornState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnEnter() {
        owner.lookAtMoveDir = true;

        owner.animations.ResetIK();

        owner.animator.SetBool("HangingFromEdge", false);
        owner.animator.SetBool("Falling", true);
        doubleJumps = owner.jumpAmount;
    }

    public override void OnExit() {
        owner.animator.SetBool("Falling", false);
        owner.animator.SetTrigger("TouchedGround");
    }

    public override void OnUpdate() {
        owner.velocity.x *= owner.airDrag;
        owner.velocity.z *= owner.airDrag;

        //inputs
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;

        owner.velocity += (owner.YRotationParent.right * input.x + owner.YRotationParent.forward * input.z) * owner.airbornSpeed;
        owner.velocity.x = Mathf.Clamp(owner.velocity.x, -owner.maxSpeed, owner.maxSpeed);
        owner.velocity.z = Mathf.Clamp(owner.velocity.z, -owner.maxSpeed, owner.maxSpeed);

        owner.velocity.y += owner.gravity * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && doubleJumps > 0) {
            owner.velocity += new Vector3(0, Mathf.Sqrt((owner.jumpHeight / 2) * -2 * owner.gravity), 0);
            doubleJumps--;
        }

        if (owner.evaluator.TouchedRoof() && owner.velocity.y > 0) {
            owner.velocity.y = 0;
        }

        base.OnUpdate();
    }
}