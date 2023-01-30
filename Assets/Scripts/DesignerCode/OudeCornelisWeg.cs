using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OudeCornelisWeg : MonoBehaviour
{
    bool replaced = false;
    public GameObject cornelisOud;
    public GameObject cornelisNieuw;
    public GameObject[] andereCornies;

    private void Start()
    {
        foreach(GameObject cornie in andereCornies)
        {
            cornie.SetActive(false);
        }

        cornelisNieuw.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !replaced)
        {
            cornelisOud.SetActive(false);
            replaced = true;
            cornelisNieuw.SetActive(true);

        }
    }
}
