using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoekUI : MonoBehaviour
{
    public float whenToHide = 1.5f;
    public GameObject boek;
    private void Awake()
    {
        //StartCoroutine(HideMeAgain());
        
    }

    IEnumerator HideMeAgain()
    {
        yield return new WaitForSeconds(whenToHide);
        boek.SetActive(false);
       

    }
}
