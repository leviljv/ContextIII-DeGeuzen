using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeClue : MonoBehaviour, IInteractable
{
    public ScriptableObject scriptableObject;
    public NoteBook noteBook;
    public GameObject DragableCluePrefab;
    public Transform notebookToggle;
    GameObject test;
    public bool ClueFound = false;

    public void Interact()
    {
        Debug.Log("talking");
        //noteBook.ToggleClueFound(scriptableObject);
        if(ClueFound == false)
        {
            test = Instantiate(DragableCluePrefab, notebookToggle);
            test.GetComponent<ClueAnswer>().ClueScriptableObject = (Clue)scriptableObject;
            test.GetComponent<DragableItem>().clue = (Clue)scriptableObject;
            ClueFound = true;
        }
            
    }
}
