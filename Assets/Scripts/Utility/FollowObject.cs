using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform ObjectToFollow;
    public float followSpeed;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, ObjectToFollow.transform.position, followSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, ObjectToFollow.eulerAngles.y, 0f);
    }
}