using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public StateMachine<T> stateMachine { get; protected set; }
    public List<Transition<T>> transitions = new List<Transition<T>>();

    public State (StateMachine<T> owner) {
        this.stateMachine = owner;
    }

    public virtual void AddTransition(Transition<T> transition) {
        transitions.Add(transition);
    }
}