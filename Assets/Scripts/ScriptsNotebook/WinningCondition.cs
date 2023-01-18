using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningCondition : MonoBehaviour
{
    public List<ClueAnswerSO> CorrectAnswers = new List<ClueAnswerSO>();
    public List<GameObject> CorrectGameObjects = new List<GameObject>();
    private NoteBookV2 noteBook;
    private ClueHolder clueHolder;
    public int index;
    public int correctAnswersFromLists;
    private bool Win = false;
    void Start()
    {
        noteBook = GetComponentInParent<NoteBookV2>();
        clueHolder = GetComponent<ClueHolder>();

    }

    // Update is called once per frame
    void Update()
    {
        if (index == 2)
        {
            foreach (GameObject obj in CorrectGameObjects)
            {
                obj.GetComponent<Image>().color = Color.green;
                Destroy(obj.GetComponent<DragableItem>());
            }
            index = 0;
        }


        if (clueHolder.ClueList.Count == CorrectAnswers.Count && Win == false)
        {
            foreach (GameObject obj in CorrectGameObjects)
            {
                obj.GetComponent<Image>().color = Color.green;
                Destroy(obj.GetComponent<DragableItem>());
            }
            EventManager.Invoke(EventType.UP_GLOBAL_INDEX);
            Debug.Log("YOU WIN");
            Win = true;
        }
    }
}
