using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClimbingTutorial : MonoBehaviour
{
    public string[] tutorialSentences;
    public int sentenceIndex = 0;
    public GameObject scrollUI;
    public TextMeshProUGUI textUI;
    public float secondsToShowScroll = 1;
    bool alreadyShownTutorial = false;

    void Start()
    {
        scrollUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        textUI.SetText(tutorialSentences[sentenceIndex]);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !alreadyShownTutorial)
        {
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, true);
            alreadyShownTutorial = true;
            StartCoroutine(ShowTutorialScroll());
        }
    }


    IEnumerator ShowTutorialScroll()
    {
        scrollUI.SetActive(true);
        yield return new WaitForSeconds(secondsToShowScroll);
        EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, false);
        scrollUI.SetActive(false);
    }
}
