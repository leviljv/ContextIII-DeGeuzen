using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCameraTrigger : MonoBehaviour
{
    private string CurrentHover;

    private bool CanInvoke = true;

    private void OnEnable() {
        EventManager.Subscribe(EventType.ON_DIALOG_ENDED, Reset);
    }
    private void OnDisable() {
        EventManager.Unsubscribe(EventType.ON_DIALOG_ENDED, Reset);
    }

    void Update() {
        if (!CanInvoke)
            return;

        CurrentHover = Hovering();

        if (CurrentHover == null)
            return;

        if (Input.GetKeyDown(KeyCode.E)) {
            CanInvoke = false;
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

    private void Reset() {
        CanInvoke = true;
    }
}