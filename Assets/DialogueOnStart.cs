using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOnStart : MonoBehaviour
{
    private void Start()
    {
        EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, "introDialoog");
    }
}
