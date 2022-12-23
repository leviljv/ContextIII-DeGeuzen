using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour {
    private bool CurrentHover;
    public float Range;

    void Update() {
        CurrentHover = Hovering();
    }

    public bool Hovering() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Range)) {
            var hitContainer = hit.transform.GetComponent<OpenDoor>();
            if (hitContainer) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    hitContainer.ToggleDoor();
                    return true;
                }
            }
        }
        return false;
    }
}