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
    private Color Groen;
    void Start()
    {
        noteBook = GetComponentInParent<NoteBookV2>();
        clueHolder = GetComponent<ClueHolder>();
        Groen = new Color32(97, 198, 106, 200);
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 2)
        {
            foreach (GameObject obj in CorrectGameObjects)
            {
                obj.GetComponent<Image>().color = Groen;
                Destroy(obj.GetComponent<DragableItem>());
            }
            index = 0;
        }


        if (clueHolder.ClueList.Count == CorrectAnswers.Count && Win == false)
        {
            foreach (GameObject obj in CorrectGameObjects)
            {
                obj.GetComponent<Image>().color = Groen;
                Destroy(obj.GetComponent<DragableItem>());
            }
            Debug.Log("YOU WIN");
            Win = true;
            //EventManager.Invoke(EventType.UP_GLOBAL_INDEX);
            EventManager<string>.Invoke(EventType.SET_SETTING, "FoundAllClues");
        }
    }
}
