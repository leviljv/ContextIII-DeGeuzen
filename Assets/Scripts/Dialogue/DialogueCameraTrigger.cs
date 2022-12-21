using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCameraTrigger : MonoBehaviour
{
    private string CurrentHover;

    void Update() {
        CurrentHover = Hovering();

        if (Input.GetKeyDown(KeyCode.E)) {
            EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, CurrentHover);
        }
    }

    public string Hovering() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 10f)) {
            var hitContainer = hit.transform.GetComponent<DialogueContainer>();
            if (hitContainer) {
                return hitContainer.DialogueName;
            }
        }
        return null;
    }
}