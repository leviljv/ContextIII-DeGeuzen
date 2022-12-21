using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClueAnswer : MonoBehaviour
{
    public Clue ClueScriptableObject;
    public TMP_Text ShowAnswer;
    public string Antwoord;

    private void Start()
    {
        Antwoord = ClueScriptableObject.ClueAntwoord;
        ShowAnswer.text = ClueScriptableObject.ClueAntwoord;
    }

    
}
