using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueDisplay : MonoBehaviour
{
    public Clue clue;
    public TMP_Text ClueVraag;
    public TMP_Text ClueAntwoord;

    // Start is called before the first frame update
    void Start(){
        ClueVraag.text = clue.ClueVraag;
        ClueAntwoord.text = clue.ClueAntwoord;
    }
}
