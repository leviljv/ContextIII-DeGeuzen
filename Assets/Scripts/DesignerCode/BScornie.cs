using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BScornie : MonoBehaviour
{
    bool replaced = false;
    public GameObject cornelisOud;
    public GameObject cornelisNieuw;
    public GameObject[] KBnMOBcornies;

    private void Start()
    {
        foreach (GameObject cornie in KBnMOBcornies)
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
