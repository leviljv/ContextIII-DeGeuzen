using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    public int currentCodexPage;
    public int numberOfPages;
    public ClueManager clueManager;
    public CollectibleManager collectibleManager;
    private NoteBookV2 noteBookV2;
    private void OnEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("TEST");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCodexPage = 0;
        numberOfPages = transform.childCount;
        noteBookV2 = GetComponentInParent<NoteBookV2>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToNextCodexPage()
    {
        if (currentCodexPage != numberOfPages - 1)
        {
            Debug.Log("Codex next page");

            currentCodexPage++;
            gameObject.transform.GetChild(currentCodexPage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCodexPage - 1).gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("End");
        }
    }

    public void GoToPrevCodexPage()
    {
        if (currentCodexPage > 0)
        {
            currentCodexPage--;
            gameObject.transform.GetChild(currentCodexPage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCodexPage + 1).gameObject.SetActive(false);
        }
        else
        {
            collectibleManager.gameObject.transform.GetChild(clueManager.numberOfPages - 1).gameObject.SetActive(true);
            SwitchToCollectible();
        }

    }

    public void SwitchToCollectible()
    {
        noteBookV2.collectibleActive = true;
        noteBookV2.codexActive = false;
        collectibleManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
