using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    private List<TakeCollectible> collectablesInScene;
    private int collectableAmountFound;

    private void OnEnable() {
        EventManager.Subscribe(EventType.COLLETABLE_FOUND, UpdateAmount);
    }

    void Start() {
        var tmp = FindObjectsOfType(typeof(TakeCollectible)) as TakeCollectible[];

        if (tmp == null)
            return;

        collectablesInScene = new(tmp);
        text.text = collectableAmountFound.ToString() + "/" + collectablesInScene.Count.ToString();
    }

    void UpdateAmount(){
        collectableAmountFound++;
        text.text = collectableAmountFound.ToString() + "/" + collectablesInScene.Count.ToString();
    }
}