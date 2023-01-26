using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_KetterBevrijd : MonoBehaviour
{
    void Start()
    {
        EventManager<string>.Invoke(EventType.ON_DIALOG_STARTED, "StartDialoogKetterBevrijd");
    }

   
}
