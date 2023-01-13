using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCameraTrigger : MonoBehaviour
{
    private string CurrentHover;

    void Update() {
        CurrentHover = Hovering();

        if (CurrentHover == null)
            return;

        if (Input.GetKeyDown(KeyCode.E)) {
            EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, CurrentHover);
        }
    }

    public string Hovering() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 10f)) {
            var hitContainer = hit.transform.GetComponent<DialogueContainer>();
            if (hitContainer) {
                if(hitContainer.GetDialog() != null || hitContainer.GetDialog() != "")
                    return hitContainer.GetDialog();
            }
        }
        return null;
    }
}