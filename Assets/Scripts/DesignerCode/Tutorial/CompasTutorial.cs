using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompasTutorial : MonoBehaviour
{
    

    private void Awake()
    {
        
    }

    private void Start()
    {
        EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, "KompasTut");
    }

}
