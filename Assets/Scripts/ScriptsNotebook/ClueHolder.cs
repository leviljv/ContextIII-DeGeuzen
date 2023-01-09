using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueHolder : MonoBehaviour
{
    public List<Clue> ClueList = new List<Clue>();
    public List<ScriptableObject> ClueAnswerList = new List<ScriptableObject>();
    public GameObject CluePrefab;
    public GameObject AnswerPrefab;
    private List<GameObject> SpawnedPrefabs = new List<GameObject>();

    //public Transform PrefabSpawnLocation;

    private void Start()
    {
        foreach (Clue clue in ClueList)
        {
            CluePrefab.GetComponent<InventorySlot>().ClueScriptableObject = clue;
            var tmp = Instantiate(CluePrefab, gameObject.transform);

            SpawnedPrefabs.Add(tmp);
        }

        
    }

    public void SpawnAnswer(ScriptableObject answer)
    {
        AnswerPrefab.GetComponent<DragableItem>().clue = (ClueAnswerSO)answer;
        AnswerPrefab.GetComponent<ClueAnswer>().ClueScriptableObject = (ClueAnswerSO)answer;
        var tmp = Instantiate(AnswerPrefab, gameObject.transform);
    }


    //private void OnDisable()
    //{
    //    foreach (GameObject prefab in SpawnedPrefabs)
    //    {
    //        Destroy(prefab);
    //    }
    //}

}
