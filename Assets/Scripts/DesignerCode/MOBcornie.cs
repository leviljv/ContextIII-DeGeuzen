using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOBcornie : MonoBehaviour
{
    bool replaced = false;
    public GameObject cornelisOud;
    public GameObject cornelisNieuw;
    public GameObject[] KBnBScornies;

    private void Start()
    {
        foreach (GameObject cornie in KBnBScornies)
        {
            cornie.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !replaced)
        {
            cornelisOud.SetActive(false);
            replaced = true;
            cornelisNieuw.SetActive(true);

        }
    }
}
