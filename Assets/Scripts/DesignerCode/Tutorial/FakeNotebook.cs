using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeNotebook : MonoBehaviour
{
    public GameObject placeholderNotebook;
    public GameObject tutorialInfo2;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            placeholderNotebook.SetActive(true);
            tutorialInfo2.SetActive(true);
        }    
    }
}
