using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool useStaticBillboarding = true;
    public float dis;
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    private void LateUpdate()  {
        if (Vector3.Distance(cam.transform.position, transform.position) > dis)
            return;

        if(!useStaticBillboarding) {
            transform.LookAt(cam.transform);
        }
        else {
            transform.rotation = cam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0f);
    }
}