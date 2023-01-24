using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFiller : MonoBehaviour
{
    public GameObject Container;
    public GameObject Prefab;

    public int amount;

    void Start() {
        for (int i = 0; i < amount; i++) {
            var parentRect = Container.GetComponent<RectTransform>();

            var tmp = Instantiate(Prefab, Container.transform);
            var rect = tmp.GetComponent<RectTransform>();

            if (amount % 2 != 0)
                rect.localPosition = new Vector2(0, (parentRect.sizeDelta.y / amount) * ((amount - 1 - i) - (amount) / 2));
            else
                rect.localPosition = new Vector2(0, (parentRect.sizeDelta.y / amount) * ((amount - 1 - i) - (amount) / 2) + (parentRect.sizeDelta.y / 2) / amount);

            rect.sizeDelta = new Vector2(parentRect.sizeDelta.x, (parentRect.sizeDelta.y / amount - 1));
        }
    }
}