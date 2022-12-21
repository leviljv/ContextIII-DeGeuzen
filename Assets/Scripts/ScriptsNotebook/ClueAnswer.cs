using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClueAnswer : MonoBehaviour
{
    public Clue ClueScriptableObject;
    public TMP_Text Antwoord;

    private void Start()
    {
        Debug.Log("test");
        Antwoord.text = ClueScriptableObject.ClueAntwoord;
    }

    
}
