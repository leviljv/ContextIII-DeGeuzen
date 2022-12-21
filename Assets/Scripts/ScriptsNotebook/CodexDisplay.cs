using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CodexDisplay : MonoBehaviour
{
    public Codex codex;
    public TMP_Text Woord;
    public TMP_Text Uitleg;


    // Start is called before the first frame update
    void Start()
    {
        Woord.text = codex.word;
        Uitleg.text = codex.description;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
