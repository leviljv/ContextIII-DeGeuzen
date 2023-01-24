using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverOver : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float Range;

    public bool ShowText = true;

    private void OnEnable() {
        EventManager.Subscribe(EventType.ON_DIALOG_STARTED, Hide);
        EventManager.Subscribe(EventType.ON_DIALOG_ENDED, Show);
    }
    private void OnDisable() {
        EventManager.Unsubscribe(EventType.ON_DIALOG_STARTED, Hide);
        EventManager.Unsubscribe(EventType.ON_DIALOG_ENDED, Show);
    }

    void Update() {
        if (!ShowText) {
            text.gameObject.SetActive(false);
            return;
        }

        Hover();
    }

    private void Hover() {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Range)) {
            var tmp = hit.collider.GetComponent<ContextContainer>();

            if(tmp != null) {
                text.gameObject.SetActive(true);
                text.text = tmp.infoToDisplay;
            }
            else {
                text.gameObject.SetActive(false);
            }
        }
        else {
            text.gameObject.SetActive(false);
        }
    }

    private void Show() {
        ShowText = true;
    }
    private void Hide() {
        ShowText = false;
    }
}