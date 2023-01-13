using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public List<string> DialogPerIndex = new();
    public List<Transform> PositionsPerIndex = new();

    private int dialogIndex = 0;
    private int positionIndex = 0;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_GLOBAL_INDEX, SetIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_GLOBAL_INDEX, SetIndex);
    }

    private void SetIndex(int index) {
        if (index > DialogPerIndex.Count - 1)
            dialogIndex = DialogPerIndex.Count - 1;
        else
            dialogIndex = index;

        if (index > PositionsPerIndex.Count - 1) {
            positionIndex = PositionsPerIndex.Count - 1;
        }
        else
            positionIndex = index;

        transform.position = PositionsPerIndex[positionIndex].position;
    }

    public string GetDialog() {
        return DialogPerIndex[dialogIndex];
    }
}