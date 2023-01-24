using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndManager : MonoBehaviour
{
    //show credits na bepaalde tijd 
    public GameObject credits;
    public float timeToStartCredits = 5;
    private void Awake()
    {
        StartCoroutine(RollCredits());
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(1.0f);

    }
}
