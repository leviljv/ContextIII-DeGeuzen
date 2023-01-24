using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnIndex : MonoBehaviour
{
    public List<bool> ActiveAtIndex;
    public GameObject toggle;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_GLOBAL_INDEX, CheckIndex);
    }

    private void CheckIndex(int index) {
        Debug.Log(index);

        if(ActiveAtIndex.Count - 1 >= index)
            if (ActiveAtIndex[index]) {
                toggle.SetActive(true);

                Debug.Log("ZET AAN");

                return;
            }

        toggle.SetActive(false);
    }
}