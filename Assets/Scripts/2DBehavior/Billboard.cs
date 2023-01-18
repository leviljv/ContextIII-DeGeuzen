using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool useStaticBillboarding = true;
    public float dis;
    public float rotSpeed;
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    private void LateUpdate()  {
        if (Vector3.Distance(cam.transform.position, transform.position) > dis)
            return;

        var tmp = Vector3.MoveTowards(transform.position, cam.transform.position, rotSpeed * Time.deltaTime);

        if(!useStaticBillboarding) {
            transform.LookAt(tmp);
        }
        else {
            transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, rotSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0f);
    }
}