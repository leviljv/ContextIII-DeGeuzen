using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OudeCornelisWeg : MonoBehaviour
{
    bool replaced = false;
    public GameObject cornelisOud;
    public GameObject cornelisNieuw;

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
