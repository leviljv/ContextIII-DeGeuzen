using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookV2 : MonoBehaviour
{
    public static NoteBookV2 instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject bookUI;

    public Transform ToggleParent;
    public ClueManager clueManager;
    public CollectibleManager collectibleManager;
    public CodexManager codexManager;
    public CollectibleHolder aaah;
    public GameObject clueAnswerPrefab;
    public AudioManager AManager;

    public List<ClueAnswerSO> test = new List<ClueAnswerSO>();
    public int CollectiblesFound = 0;
    public int CollectibleChildNumber = 0;

    [HideInInspector] public bool BookActive = false;
    public bool collectibleActive;
    public bool codexActive;

    void Start()
    {
        ToggleParent.gameObject.SetActive(false);
        collectibleActive = false;
        codexActive = false;
        bookUI.SetActive(false);
        AManager.Init();
    }

    void Update()
    {
        ActivateBook();
    }

    public void SetClue(ClueAnswerSO clueAnswer)
    {
        int QuestNumber = clueAnswer.QuestNumber;
        if (!clueManager.gameObject.transform.GetChild(QuestNumber).GetComponent<ClueHolder>().ClueAnswerList.Contains(clueAnswer))
        {
            clueManager.gameObject.transform.GetChild(QuestNumber).GetComponent<ClueHolder>().ClueAnswerList.Add(clueAnswer);
            clueManager.gameObject.transform.GetChild(QuestNumber).GetComponent<ClueHolder>().SpawnAnswer(clueAnswer);
            bookUI.SetActive(true);
            ZetCoroutineAan();
        }
    }

    //sorry jogchum
    void ZetCoroutineAan()
    {
        StartCoroutine(HideBookUI());
    }
    IEnumerator HideBookUI()
    {
        yield return new WaitForSeconds(1);
        bookUI.SetActive(false);
        StopCoroutine(HideBookUI());
    }

    public void ActivateBook()
    {
        if (Input.GetKeyDown(KeyCode.Q) && BookActive == false)
        {
            AManager.PlayAudio("Open");
            ToggleParent.gameObject.SetActive(true);
            BookActive = true;
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, true);
        }

        else if (Input.GetKeyDown(KeyCode.Q) && BookActive == true)
        {
            AManager.PlayAudio("Close");
            ToggleParent.gameObject.SetActive(false);
            BookActive = false;
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && BookActive == true)
        {
            AManager.PlayAudio("Flip");
            PageLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && BookActive == true)
        {
            AManager.PlayAudio("Flip");
            PageRight();
        }
    }

    public void PageRight()
    {
        if (collectibleActive == false && codexActive == false)
        {
            clueManager.GoToNextCluePage();
        }
        else if (collectibleActive == true && codexActive == false)
        {
            collectibleManager.GoToNextCollectiblePage();
        }
        else if (collectibleActive == false && codexActive == true)
            codexManager.GoToNextCodexPage();
    }

    public void PageLeft()
    {
        if (collectibleActive == false && codexActive == true)
            codexManager.GoToPrevCodexPage();
        else if (collectibleActive == true && codexActive == false)
            collectibleManager.GoToPrevCollectiblePage();
        else if (collectibleActive == false && codexActive == false)
            clueManager.GoToPrevCluePage();
    }
}
