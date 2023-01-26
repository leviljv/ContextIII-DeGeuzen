using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefDialogue : MonoBehaviour
{
    
    void Awake()
    {
        EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, "BriefDialogue");
    }

    
}
