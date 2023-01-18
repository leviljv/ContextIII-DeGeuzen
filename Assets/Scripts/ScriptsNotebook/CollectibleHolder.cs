using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHolder : MonoBehaviour
{
    public List<GameObject> CollectibleList = new List<GameObject>();
    public GameObject CollectiblePrefab;
    private List<GameObject> SpawnedPrefabs = new List<GameObject>();
    public int index = 0;
    //public Transform PrefabSpawnLocation;

    public void Test()
    {
        Debug.Log("VINK");
        CollectibleList[index].GetComponent<SetVinkje>().ActivateVinkje();
        index++;
    }

    
}
