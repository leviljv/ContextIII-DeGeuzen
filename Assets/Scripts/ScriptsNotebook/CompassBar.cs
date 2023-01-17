using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassBar : MonoBehaviour
{
    public GameObject iconPrefab;
    public RectTransform compassBarTransform;
    public RectTransform northTransform;
    public RectTransform southTransform;
    public GameObject player;

    public Transform camera;

    private float compassUnit;
    private List<QuestMarker> questMarkers = new List<QuestMarker>();
    public List<QuestMarker> questMarkerLocations = new List<QuestMarker>();
    

    // Start is called before the first frame update
    void Start()
    {
        compassUnit = compassBarTransform.rect.width / 360f;

        foreach (QuestMarker marker in questMarkerLocations)
        {
            AddQuestMarker(marker);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SetMarkerPosition(northTransform, Vector3.forward * 1000);
        SetMarkerPosition(southTransform, Vector3.back * 1000);

        foreach (QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - camera.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(camera.transform.forward.x, camera.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassBarTransform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }
    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerfwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerfwd);

        return new Vector2(compassUnit * angle, 0f);

    }
}
