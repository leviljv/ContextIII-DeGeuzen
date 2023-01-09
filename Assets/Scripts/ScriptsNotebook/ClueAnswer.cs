using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClueAnswer : MonoBehaviour
{
    public ClueAnswerSO ClueScriptableObject;
    public TMP_Text Antwoord;

    private void Start()
    {
        Antwoord.text = ClueScriptableObject.ClueAntwoord;
    }


}
