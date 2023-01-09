using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public int currentCollectiblePage;
    public int numberOfPages;
    public ClueManager clueManager;
    public CodexManager CodexManager;
    private NoteBookV2 noteBookV2;
    private void OnEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("TEST");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCollectiblePage = 0;
        numberOfPages = transform.childCount;
        noteBookV2 = GetComponentInParent<NoteBookV2>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToNextCollectiblePage()
    {
        if (currentCollectiblePage != numberOfPages - 1)
        {
            Debug.Log("Collectible next page");

            currentCollectiblePage++;
            gameObject.transform.GetChild(currentCollectiblePage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCollectiblePage - 1).gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Gotta go to codex");
            gameObject.transform.GetChild(currentCollectiblePage).gameObject.SetActive(false);
            SwitchToCodex();
        }
    }

    public void GoToPrevCollectiblePage()
    {
        if(currentCollectiblePage > 0)
        {
            currentCollectiblePage--;
            gameObject.transform.GetChild(currentCollectiblePage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCollectiblePage + 1).gameObject.SetActive(false);
        }
        else
        {
            noteBookV2.collectibleActive = false;
            clueManager.gameObject.transform.GetChild(clueManager.numberOfPages -1).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }

    public void SwitchToCodex()
    {
        CodexManager.gameObject.SetActive(true);
        noteBookV2.codexActive = true;
        noteBookV2.collectibleActive = false;
    }

}
