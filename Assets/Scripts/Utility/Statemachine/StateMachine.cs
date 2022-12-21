using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public State<T> currentState;
    public Dictionary<System.Type, State<T>> StateDic = new();
    public T Owner { get; protected set; }

    public StateMachine(T owner) {
        Owner = owner;
    }

    public void OnUpdate() {
        if(currentState != null)
            currentState.OnUpdate();
    }

    public void ChangeState(System.Type state) {
        if(currentState == StateDic[state]) {
            Debug.LogError("Can't go into state it's already in");
            return;
        }

        if(currentState != null)
            currentState.OnExit();

        if (StateDic.ContainsKey(state)) {
            var tmpState = StateDic[state];
            tmpState.OnEnter();
            currentState = tmpState;
            return;
        }
    }

    public void AddState(System.Type type, State<T> state) {
        if (StateDic.ContainsValue(state)) {
            Debug.LogError("Already added State");
            return;
        }
        StateDic.Add(type, state);
    }

    public void RemoveState(System.Type type) {
        if (!StateDic.ContainsKey(type)) {
            Debug.LogError("State not in the list");
            return;
        }
        StateDic.Remove(type);
    }
}