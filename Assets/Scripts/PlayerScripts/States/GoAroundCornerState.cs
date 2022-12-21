using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAroundCornerState : MoveState
{
    public GoAroundCornerState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public bool IsDone = false;

    public override void OnEnter() {


        IsDone = false;
    }

    public override void OnExit() {
        IsDone = false;
    }

    public override void OnUpdate() {


        Vector3 desiredForward = Vector3.RotateTowards(owner.transform.forward, -owner.CurrentLedge.transform.forward, 1 * Time.deltaTime, 0f);
        desiredForward.y = 0;
        owner.transform.LookAt(owner.transform.position + desiredForward);
    }
}