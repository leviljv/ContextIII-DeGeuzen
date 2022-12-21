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

    //public TMP_Text description;

    // Start is called before the first frame update
    void Start()
    {
        ClueVraag.text = clue.ClueVraag;
        ClueAntwoord.text = clue.ClueAntwoord;

        
        //description.text = collectible.description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
