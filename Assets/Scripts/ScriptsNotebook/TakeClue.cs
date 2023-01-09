using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeClue : MonoBehaviour, IInteractable
{
    public ClueAnswerSO scriptableObject;
    public GameObject DragableCluePrefab;
    public NoteBookV2 noteBook;
    GameObject tmp;
    public bool ClueFound = false;
    public int QuestNumber;

    private void Start()
    {
        QuestNumber = scriptableObject.QuestNumber;
    }

    public void Interact()
    {
        if (ClueFound == false)
        {
            noteBook.clueManager.gameObject.transform.GetChild(QuestNumber).GetComponent<ClueHolder>().ClueAnswerList.Add(scriptableObject);
            noteBook.clueManager.gameObject.transform.GetChild(QuestNumber).GetComponent<ClueHolder>().SpawnAnswer(scriptableObject);

            ClueFound = true;
        }

    }
}
