using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public int openAtIndex;

    public GameObject openbridgeModel;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }

    private void CheckIndex(int index) {
        if (index >= openAtIndex) {
            Destroy(transform.GetChild(0).gameObject);

            Instantiate(openbridgeModel, transform);
        }
    }
}