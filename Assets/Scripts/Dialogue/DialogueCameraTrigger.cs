using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCameraTrigger : MonoBehaviour
{
    private DialogueContainer CurrentHover;
    private Transform ObjectInteractingWith;

    private bool CanInvoke = true;

    private void OnEnable() {
        EventManager.Subscribe(EventType.ON_DIALOG_ENDED, Reset);
    }
    private void OnDisable() {
        EventManager.Unsubscribe(EventType.ON_DIALOG_ENDED, Reset);
    }

    void Update() {
        if(ObjectInteractingWith != null) {
            if (Vector3.Distance(transform.position, ObjectInteractingWith.position) > 10f) {
                EventManager.Invoke(EventType.RESET_DIALOG);
                ObjectInteractingWith = null;
                CanInvoke = true;
            }
        }

        if (!CanInvoke)
            return;

        CurrentHover = Hovering();

        if (CurrentHover == null)
            return;

        if (Input.GetKeyDown(KeyCode.E)) 
            if (CurrentHover.GetDialog() != null || CurrentHover.GetDialog() != "") {
                CanInvoke = false;
                EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, CurrentHover.GetDialog());
                EventManager<GameObject>.Invoke(EventType.ON_DIALOG_STARTED, ObjectInteractingWith.gameObject);
                EventManager.Invoke(EventType.ON_DIALOG_STARTED);
            }
    }

    public DialogueContainer Hovering() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 10f)) {
            var hitContainer = hit.transform.GetComponent<DialogueContainer>();
            if (hitContainer) {
                ObjectInteractingWith = hit.transform;
                return hitContainer;
            }
        }
        return null;
    }

    private void Reset() {
        CanInvoke = true;
    }
}