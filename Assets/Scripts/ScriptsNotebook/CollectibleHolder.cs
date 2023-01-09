using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHolder : MonoBehaviour
{
    public List<Collectible> CollectibleList = new List<Collectible>();
    public GameObject CollectiblePrefab;
    private List<GameObject> SpawnedPrefabs = new List<GameObject>();
    //public Transform PrefabSpawnLocation;

    public void OnEnable()
    {
        foreach (Collectible coll in CollectibleList)
        {
            CollectiblePrefab.GetComponent<CollectibleDisplay>().collectible = coll;

            var tmp = Instantiate(CollectiblePrefab, gameObject.transform);

            SpawnedPrefabs.Add(tmp); 
        }
    }

    public void OnDisable()
    {
        foreach (GameObject prefab in SpawnedPrefabs)
        {
            Destroy(prefab);
        }
    }
}
