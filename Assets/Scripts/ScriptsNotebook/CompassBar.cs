using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassBar : MonoBehaviour
{
    public RectTransform compassBarTransform;

    public RectTransform objectiveMarkerTransform;
    public RectTransform northTransform;
    public RectTransform southTransform;

    public Transform camera;
    public Transform objectiveTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetMarkerPosition(objectiveMarkerTransform, objectiveMarkerTransform.position);
        SetMarkerPosition(northTransform, Vector3.forward * 1000);
        SetMarkerPosition(southTransform, Vector3.back * 1000);
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - camera.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(camera.transform.forward.x, camera.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }
}
