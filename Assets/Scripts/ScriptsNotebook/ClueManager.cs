using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public int currentCluePage;
    public int numberOfPages;
    public CollectibleManager collectibleManager;

    private NoteBookV2 noteBookV2;

    private void OnEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Awake()
    {
        numberOfPages = transform.childCount;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCluePage = 0;
        noteBookV2 = GetComponentInParent<NoteBookV2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToNextCluePage()
    {
        if(currentCluePage != numberOfPages -1)
        {
            currentCluePage++;
            gameObject.transform.GetChild(currentCluePage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCluePage -1).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(currentCluePage).gameObject.SetActive(false);
            SwitchToCollectible();
        }
        
    }

    public void GoToPrevCluePage()
    {
        if (currentCluePage > 0 && noteBookV2.collectibleActive == false)
        {
            currentCluePage--;
            gameObject.transform.GetChild(currentCluePage).gameObject.SetActive(true);
            gameObject.transform.GetChild(currentCluePage + 1).gameObject.SetActive(false);
        }
    }

    public void SwitchToCollectible()
    {
        collectibleManager.gameObject.SetActive(true);
        noteBookV2.collectibleActive = true;
    }
}
