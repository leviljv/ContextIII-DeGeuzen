using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType {
    EMPTY,
    ON_DIALOG_STARTED,
    ON_DIALOG_ENDED,
    DIALOG_SET_TYPE_TIME,
    DIALOG_SET_PORTRAIT,
    DIALOG_GIVE_CLUE,
    DIALOG_DO_ANIMATION,
    DIALOG_PLAY_SOUND,
    RESET_DIALOG,
    SET_INTERACTION_STATE,
    SET_GLOBAL_INDEX,
    SET_SETTING,
    UP_GLOBAL_INDEX,
    LOWER_GLOBAL_INDEX,
    NEXT_SCENE,
    COLLETABLE_FOUND,
    DO_FADE,
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