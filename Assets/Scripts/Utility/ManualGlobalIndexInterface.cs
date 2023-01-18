using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualGlobalIndexInterface : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.F5)) {
            EventManager.Invoke(EventType.UP_GLOBAL_INDEX);
        }
        if (Input.GetKeyDown(KeyCode.F6)) {
            EventManager.Invoke(EventType.LOWER_GLOBAL_INDEX);
        }
    }
}