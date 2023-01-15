using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFunctionality
{
    public DialogueSystem Owner;

    private Dictionary<string, Sprite> Portraits = new();
    private Dictionary<string, ClueAnswerSO> Clues = new();

    public void Init() {
        var portraits = Resources.LoadAll<Sprite>("Dialogue/Portraits/");
        var clues = Resources.LoadAll<ClueAnswerSO>("Dialogue/Clues/");
        if (portraits.Length < 1) {
            Debug.LogError("No Files");
            return;
        }

        foreach (var item in portraits) {
            Portraits.Add(item.name, item);
        }
        foreach (var item in clues) {
            Clues.Add(item.name, item);
        }

        SetEvents();
    }

    public void SetEvents() {
        EventManager<float>.Subscribe(EventType.DIALOG_SET_TYPE_TIME, SetTimeBetweenChars);
        EventManager<string>.Subscribe(EventType.DIALOG_SET_PORTRAIT, SetSprite);
        EventManager<string>.Subscribe(EventType.DIALOG_GIVE_CLUE, GiveClue);
    }

    public void SetTimeBetweenChars(float speed) {
        Owner.currentTimeBetweenChars = speed;
    }

    public void SetSprite(string SpriteName) {
        if (Portraits.ContainsKey(SpriteName)) {
            Owner.Portrait.sprite = Portraits[SpriteName];
        }
        else {
            Debug.LogError("Portrait: " + SpriteName + " Not Present In Dictionairy");
        }
    }

    public void GiveClue(string ClueName)
    {
        if (Clues.ContainsKey(ClueName))
        {
            var pickedClue = Clues[ClueName];
            NoteBookV2.instance.SetClue(pickedClue);
        }
        else
        {
            Debug.LogError("Clue: " + ClueName + " Not Present In Dictionairy");
        }
    }
}