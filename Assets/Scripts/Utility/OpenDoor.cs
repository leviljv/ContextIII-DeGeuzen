using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Quaternion openRot;

    private bool isOpen;
    private Quaternion startingRot;

    private void Start() {
        startingRot = transform.rotation;

        openRot = startingRot * openRot;
    }

    private void Update() {
        if (isOpen)
            transform.rotation = Quaternion.Lerp(transform.rotation, openRot, 20 * Time.deltaTime);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, startingRot, 20 * Time.deltaTime);
    }

    public void ToggleDoor() {
        isOpen = !isOpen;
    }
}