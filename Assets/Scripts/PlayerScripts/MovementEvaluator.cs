using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEvaluator 
{
    public MovementManager owner;
    public List<GameObject> currentCollisions;

    public Vector3 GetSlopeNormal() {
        Vector3 origin = owner.controller.transform.position + new Vector3(0, .1f, 0);

        Physics.Raycast(origin, Vector3.down, out var hit, 1f);
        return IsGrounded() ? hit.normal : Vector3.up;
    }

    public bool IsGrounded() {
        if (Physics.CheckSphere(owner.transform.position + new Vector3(0, .2f, 0), .5f, owner.GroundLayer)) {
            return true;
        }

        return false;
    }

    public bool TouchedRoof() {
        if (Physics.CheckSphere(owner.transform.position + new Vector3(0, 1.6f, 0), .5f, owner.GroundLayer)) {
            return true;
        }

        return false;
    }

    public GameObject CollectableNearby() {
        var tmp = new List<Collider>(Physics.OverlapSphere(owner.LedgeCheck.transform.position, owner.collectableRadius));

        for (int i = tmp.Count - 1; i >= 0; i--) {
            if (tmp[i].GetComponent<IInteractable>() != null)
                continue;
                
            tmp.RemoveAt(i);
        }

        if (tmp.Count < 1)
            return null;

        return tmp[0].gameObject;
    }

    public GameObject SphereCast(Vector3 input, float dis, float rad) {
        if (input.magnitude < .1f)
            return null;

        if (Input.GetKey(KeyCode.Space)) {
            dis = 3;
            rad = rad * 3;
        }

        var pos = owner.LedgeCheck.transform.position + owner.LedgeCheck.transform.TransformDirection(input.normalized * .7f) * dis;

        List<Collider> tmp = new(Physics.OverlapSphere(pos, rad));
        
        if (tmp.Count < 1)
            return null;

        for (int i = tmp.Count - 1; i >= 0; i--) {
            if(tmp[i].gameObject.layer != 6) {
                tmp.RemoveAt(i);
                continue;
            }
        } 

        if (tmp.Count < 1)
            return null;

        var closestPoint = tmp[0];
        foreach (var item in tmp) {
            if (item == closestPoint)
                continue;

            if (Vector3.Distance(pos, closestPoint.transform.position) > Vector3.Distance(pos, item.transform.position)) {
                closestPoint = item;
            }
        }

        return closestPoint.gameObject;
    }

    public Vector3 CanGoOntoLedge() {
        Vector3 pos = new Vector3(owner.transform.position.x, owner.CurrentLedge.transform.position.y + .1f, owner.transform.position.z);
        Ray ray = new(pos, owner.transform.forward);

        if (!Physics.Raycast(ray, out var hit, 3f)) {
            return owner.transform.position + owner.transform.forward * 1.2f + new Vector3(0, 2.4f, 0);
        }

        return Vector3.zero;
    }
}