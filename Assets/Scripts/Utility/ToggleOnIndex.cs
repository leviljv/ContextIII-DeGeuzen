using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnIndex : MonoBehaviour
{
    public int ActiveAtIndex;
    private GameObject toggle;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }

    private void CheckIndex(int index) {
        if (index == ActiveAtIndex) 
            toggle.SetActive(true);
        else
            toggle.SetActive(true);
    }
}