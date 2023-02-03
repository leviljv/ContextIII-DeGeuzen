using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    public void Clicked()
    {
        AudioPlaceholder.instance.Clicked();
    }

    public void Hovered()
    {
        AudioPlaceholder.instance.Hovered();
    }
}
