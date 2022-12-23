using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public List<string> DialogPerIndex = new();

    private int currentIndex = 0;
    public string DialogueName;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_DIALOG_INDEX, SetIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_DIALOG_INDEX, SetIndex);
    }

    private void SetIndex(int index) {
        if (index > DialogPerIndex.Count - 1)
            currentIndex = DialogPerIndex.Count - 1;
        else
            currentIndex = index;
    }

    public string GetDialog() {
        return DialogPerIndex[currentIndex];
    }
}