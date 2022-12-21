using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : MoveState 
{ 
    public InteractionState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public override void OnEnter() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        owner.YRotationParent.GetComponent<CameraLookaround>().enabled = false;
    }

    public override void OnExit() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        owner.YRotationParent.GetComponent<CameraLookaround>().enabled = true;
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }
}
