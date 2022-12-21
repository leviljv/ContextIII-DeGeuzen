using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpOnPlatformState : MoveState {

    public GetUpOnPlatformState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public bool IsDone = false;
    public Vector3 endpoint;

    public override void OnEnter() {
        endpoint = owner.evaluator.CanGoOntoLedge();

        //owner.CurrentLedge = null;

        owner.animator.SetTrigger("GetOntoPlatform");
        IsDone = false;
    }

    public override void OnExit() {
        IsDone = false;
    }

    public override void OnUpdate() {
        if (Vector3.Distance(owner.transform.position, endpoint) > .2f) {
            owner.velocity = (endpoint - owner.transform.position).normalized * 5;
        }
        else {
            IsDone = true;
        }

        base.OnUpdate();
    }
}