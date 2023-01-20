using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompasTutorial : MonoBehaviour
{
    public string[] tutorialSentences;
    public int sentenceIndex = 0;
    public GameObject scrollUI;
    public TextMeshProUGUI textUI;
    public int kysIndex = 0;

    private void Awake()
    {
        EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, true);
    }
    void Update()
    {
        textUI.SetText(tutorialSentences[sentenceIndex]);


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            scrollUI.SetActive(true);
        }
    }
    public void NextSentence()
    {
        sentenceIndex = (sentenceIndex + 1) % tutorialSentences.Length;
        kysIndex++;   
        if (kysIndex == 4)
        {
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, false);
            scrollUI.SetActive(false);
            
        }
    }
}
