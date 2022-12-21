using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : State<MovementManager> {

    public MovementManager owner;

    public MoveState(StateMachine<MovementManager> owner) : base(owner) {
        this.owner = stateMachine.Owner;
    }

    public override void OnUpdate() {
        foreach (var transition in transitions) {
            if (transition.condition.Invoke(owner)) {
                stateMachine.ChangeState(transition.toState);
                return;
            }
        }
    }
}