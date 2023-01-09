using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{
    public List<string> CorrectAnswers = new List<string>();
    private NoteBookV2 noteBook;
    public int correctAnswersFromLists;
    void Start()
    {
        noteBook = GetComponent<NoteBookV2>();
        for (int i = 0; i < noteBook.clueManager.numberOfPages; i++)
        {
            Debug.Log("Test");
            correctAnswersFromLists += noteBook.clueManager.gameObject.transform.GetChild(i).GetComponent<ClueHolder>().ClueList.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CorrectAnswers.Count >= correctAnswersFromLists)
        {
            Debug.Log("YOU WIN");
        }
    }
}
