using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverOver : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float Range;

    void Update() {
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
}