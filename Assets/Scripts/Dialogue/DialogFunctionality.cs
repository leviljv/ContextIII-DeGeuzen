using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFunctionality
{
    public DialogSystem Owner;

    private Dictionary<string, Sprite> Portraits = new();

    public void Init() {
        var portraits = Resources.LoadAll<Sprite>("Portraits/");
        if (portraits.Length < 1) {
            Debug.LogError("No Files");
            return;
        }

        foreach (var item in portraits) {
            Portraits.Add(item.name, item);
        }

        SetEvents();
    }

    public void SetEvents() {
        EventManager<float>.Subscribe(EventType.DIALOG_SET_TYPE_TIME, SetTimeBetweenChars);
        EventManager<string>.Subscribe(EventType.DIALOG_SET_PORTRAIT, SetSprite);
    }

    public void SetTimeBetweenChars(float speed) {
        Owner.TimeBetweenChars = speed;
    }

    public void SetSprite(string SpriteName) {
        if(Portraits.ContainsKey(SpriteName))
            Owner.Portrait.sprite = Portraits[SpriteName];
    }
}