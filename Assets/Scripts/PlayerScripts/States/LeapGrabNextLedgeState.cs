using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapGrabNextLedgeState : MoveState {

    public LeapGrabNextLedgeState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public GameObject Ledge;
    public bool isDone = false;

    public override void OnEnter() {
        isDone = false;

        Ledge = owner.CurrentLedge;

        owner.animator.SetTrigger("HangJumpUp");
    }

    public override void OnExit() {
        isDone = false;
    }

    public override void OnUpdate() {
        var forwards = owner.transform.forward * .63f;

        var leftDis = Vector3.Distance((owner.transform.position + new Vector3(0, 1.6f, 0) + forwards), Ledge.transform.position + Ledge.transform.TransformDirection(new Vector3(Ledge.transform.localScale.x / 2, 0, 0)));
        var rightDis = Vector3.Distance((owner.transform.position + new Vector3(0, 1.6f, 0) + forwards), Ledge.transform.position - Ledge.transform.TransformDirection(new Vector3(Ledge.transform.localScale.x / 2, 0, 0)));

        if (Mathf.Abs(owner.transform.position.y + 1.5f - owner.CurrentLedge.transform.position.y) > .05f) {
            var dif = new Vector3(owner.CurrentLedge.transform.position.x - owner.transform.position.x, 1.5f, 0);
            var tmp = (owner.CurrentLedge.transform.position - (owner.transform.position + dif)).normalized * 5;
            owner.velocity = (new Vector3(0, tmp.y, 0) * 100).normalized * 5;
        }
        else {
            isDone = true;
        }

        base.OnUpdate();
    }
}