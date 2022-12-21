using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition<T>
{
    public Transition(System.Predicate<T> condition, System.Type toState)
    {
        this.condition = condition;
        this.toState = toState;
    }

    public System.Predicate<T> condition;
    public System.Type toState;
}
