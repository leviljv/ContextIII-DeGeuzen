using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVinkje : MonoBehaviour
{
    public Image vinkje;

    public void ActivateVinkje()
    {
        var tempColor = vinkje.color;
        tempColor.a = 1f;
        vinkje.color = tempColor;
    }
}
