using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public Clue ClueScriptableObject;
    public TMP_Text ClueVraag;
    public TMP_Text Antwoord;

    private void Start()
    {
        ClueVraag.text = ClueScriptableObject.ClueVraag;
        Antwoord.text = ClueScriptableObject.ClueAntwoord;
    }

}
