using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompasTutorial : MonoBehaviour
{
    

    private void Awake()
    {
        EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, "KompasTut");
    }
    
}
