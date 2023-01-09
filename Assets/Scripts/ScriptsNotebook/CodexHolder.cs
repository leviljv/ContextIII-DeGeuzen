using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexHolder : MonoBehaviour
{
    public List<Codex> CodexList = new List<Codex>();
    public GameObject codexPrefab;
    //public Transform PrefabSpawnLocation;

    public void Start()
    {
        foreach (Codex codex in CodexList)
        {
            codexPrefab.GetComponent<CodexDisplay>().codex = codex;
            var tmp = Instantiate(codexPrefab, gameObject.transform);
        }
    }
}
