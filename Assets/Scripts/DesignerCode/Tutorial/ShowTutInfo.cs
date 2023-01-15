using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowTutInfo : MonoBehaviour
{
    private TutorialInfoContainer tutorialInfoContainer;
    public GameObject scroll;
    public string[] tutorialStrings;
    [HideInInspector] public int stringIndex = 0;
    private int convoIndex = 0;
    public TextMeshProUGUI tutorialText;

    private void Start()
    {
        tutorialInfoContainer = GameObject.Find("TutorialInfoList").GetComponent<TutorialInfoContainer>();
    }
    private void Update()
    {
        tutorialText.SetText(tutorialStrings[stringIndex]);

        if(convoIndex >= tutorialStrings.Length)
        {
            tutorialInfoContainer.CycleCurrentTutorialInfo();
            scroll.SetActive(false);
        }
    }

    public void NextString()
    {
        convoIndex++;
        stringIndex = (stringIndex + 1) % tutorialStrings.Length;
    }

}
