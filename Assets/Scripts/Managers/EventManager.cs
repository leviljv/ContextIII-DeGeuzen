using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType {
    EMPTY,
    ON_DIALOG_STARTED,
    DIALOG_SET_TYPE_TIME,
    DIALOG_SET_PORTRAIT,
    SET_INTERACTION_STATE,
}

public static class EventManager {
    private static readonly Dictionary<EventType, System.Action> eventActions = new();

    public static void Subscribe(EventType eventType, System.Action eventToSubscribe) {
        if (!eventActions.ContainsKey(eventType)) {
            eventActions.Add(eventType, null);
        }
        eventActions[eventType] += eventToSubscribe;
    }

    public static void Unsubscribe(EventType eventType, System.Action eventToUnsubscribe) {
        if (eventActions.ContainsKey(eventType)) {
            eventActions[eventType] -= eventToUnsubscribe;
        }
    }

    public static void Invoke(EventType eventType) {
        if (eventActions.ContainsKey(eventType))
            eventActions[eventType]?.Invoke();
    }
}

public static class EventManager<T> {
    private static readonly Dictionary<EventType, System.Action<T>> eventActions = new();

    public static void Subscribe(EventType eventType, System.Action<T> eventToSubscribe) {
        if (!eventActions.ContainsKey(eventType)) {
            eventActions.Add(eventType, null);
        }
        eventActions[eventType] += eventToSubscribe;
    }

    public static void Unsubscribe(EventType eventType, System.Action<T> eventToUnsubscribe) {
        if (eventActions.ContainsKey(eventType)) {
            eventActions[eventType] -= eventToUnsubscribe;
        }
    }

    public static void Invoke(EventType eventType, T obj) {
        if (eventActions.ContainsKey(eventType))
            eventActions[eventType]?.Invoke(obj);
    }
}