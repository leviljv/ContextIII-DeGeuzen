using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNextLedgeState : MoveState
{
    public GrabNextLedgeState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public bool isDone = false;
    public GameObject Ledge;

    float dis;

    public override void OnEnter() {
        isDone = false;

        Ledge = owner.CurrentLedge;
        dis = Vector3.Distance(owner.CurrentLedge.transform.position, owner.LedgeCheck.transform.position);

        owner.canGrabNextLedge = false;
    }

    public override void OnExit() {
        if (dis > 1)
            owner.ResetTimer(owner.climbTime);
        else
            owner.ResetTimer(owner.climbTime / 2);
        isDone = false;
    }

    public override void OnUpdate() {
        var offset = owner.CurrentLedge.transform.position - owner.LedgeCheck.transform.position;
        if (offset.magnitude > .05f) {
            owner.velocity = offset.normalized * Vector3.Distance(owner.CurrentLedge.transform.position , owner.LedgeCheck.transform.position) * 3;
        }
        else {
            isDone = true;
        }

        Vector3 desiredForward = Vector3.RotateTowards(owner.transform.forward, -owner.CurrentLedge.transform.forward, 1 * Time.deltaTime, 0f);
        desiredForward.y = 0;
        owner.transform.LookAt(owner.transform.position + desiredForward);

        base.OnUpdate();
    }
}