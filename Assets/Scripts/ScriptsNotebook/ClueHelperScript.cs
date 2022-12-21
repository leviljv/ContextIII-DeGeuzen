using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueHelperScript : MonoBehaviour
{
    public List<TakeClue> ClueFoundList = new List<TakeClue>();

    public void AddObjectToList(TakeClue clueObject)
    {
        ClueFoundList.Add(clueObject);
    }
}
