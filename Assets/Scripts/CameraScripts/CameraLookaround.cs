using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookaround : MonoBehaviour 
{
    public Transform ObjectToFollow;
    public Transform XRotTransform;

    public Transform player;
    public bool Enabled;

    public float mouseSensitivity = 100f;
    public float followSpeed;
    public float MaxAngleLook = 65f;
    public float MinAngleLook = 65f;
    
    public bool turnPlayer;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start() {
        Enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (!Enabled)
            return;

        if (followSpeed > 0) {
            transform.position = Vector3.Lerp(transform.position, ObjectToFollow.transform.position, followSpeed * Time.deltaTime);
            CheckBackwards();
        }
        else
            transform.position = ObjectToFollow.transform.position;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, MinAngleLook, MaxAngleLook);

        XRotTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if(turnPlayer)
            player.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void CheckBackwards() {
        if(Physics.Raycast(XRotTransform.position, -XRotTransform.forward, out var hit, 4f)) {
            if(Vector3.Distance(XRotTransform.position, hit.point) < 4)
                Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -Vector3.Distance(XRotTransform.position, hit.point) + .5f);
        }
        else {
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -4);
        }
    }
}