using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public Sprite empty;
    public Sprite marker;

    public List<string> DialogPerIndex = new();
    public List<Transform> PositionsPerIndex = new();
    public List<bool> ShowMarkerPerIndex = new();

    private GameObject Marker;

    private int dialogIndex = 0;
    private int positionIndex = 0;
    private int markerIndex = 0;

    private void OnEnable() {
        EventManager<int>.Subscribe(EventType.SET_GLOBAL_INDEX, SetIndex);
    }
    private void OnDisable() {
        EventManager<int>.Unsubscribe(EventType.SET_GLOBAL_INDEX, SetIndex);
    }

    private void Start() {
        if(transform.childCount > 0)
            Marker = transform.GetChild(transform.childCount - 1).gameObject;
    }

    private void SetIndex(int index) {
        if(DialogPerIndex.Count > 0) {
            if (index > DialogPerIndex.Count - 1)
                dialogIndex = DialogPerIndex.Count - 1;
            else
                dialogIndex = index;
        }

        if (PositionsPerIndex.Count > 0) {
            if (index > PositionsPerIndex.Count - 1)
                positionIndex = PositionsPerIndex.Count - 1;
            else
                positionIndex = index;

            transform.position = PositionsPerIndex[positionIndex].position;
        }

        if (ShowMarkerPerIndex.Count > 0) {
            if (index > ShowMarkerPerIndex.Count - 1)
                markerIndex = ShowMarkerPerIndex.Count - 1;
            else
                markerIndex = index;

            GetComponent<QuestMarker>().image.sprite = marker;
            Marker.SetActive(ShowMarkerPerIndex[markerIndex]);
        }
    }

    public string GetDialog() {
        GetComponent<QuestMarker>().image.sprite = empty;
        Marker.SetActive(false);
        return DialogPerIndex[dialogIndex];
    }
}