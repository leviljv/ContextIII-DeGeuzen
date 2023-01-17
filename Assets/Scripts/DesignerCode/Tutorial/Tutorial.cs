using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    public string[] tutorialSentences;
    public int sentenceIndex = 0;
    public GameObject scrollUI;
    public GameObject bookUI;
    public GameObject answer;
    public TextMeshProUGUI textUI;
    public WinningCondition winCondition;

    bool showingNotebook;
    private void Start()
    {
        answer.SetActive(false);
        bookUI.SetActive(false);
    }

    /* vergeef mij voor deze verschrikkelijke code, ik kan niet nadenken en dan komen dit soort gedrochten uit mijn ziel. weet je soms dan moet je gewoon even iets schrijven waar een dev enorm verdrietig van wordt, en waar je zelf heel blij van wordt omdat tja het werkt toch ookal doet het pijn aan je dev z'n ziel. je mag het opnieuw schrijven, maar laat me dan even weten hoe je het op de goede manier doet zodat ik iets leer! */
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !showingNotebook)
        {
            showingNotebook = true;
            scrollUI.SetActive(true);
            sentenceIndex++;
        }

        

        textUI.SetText(tutorialSentences[sentenceIndex]);


        if(sentenceIndex == 1)
        {
            bookUI.SetActive(true);
        }
        else if(sentenceIndex == 4)
        {
            answer.SetActive(true);
        }
        else if(sentenceIndex == 6)
        {
            if (winCondition.CorrectAnswers.Count >= 1)
            {
                Debug.Log("slay");
                scrollUI.SetActive(true);
            }
        }
    }

    public void NextSentence()
    {
        sentenceIndex = (sentenceIndex + 1) % tutorialSentences.Length;
        if(sentenceIndex == 2)
        {
            scrollUI.SetActive(false);
        }
        else if (sentenceIndex == 6)
        {
            scrollUI.SetActive(false);
        }
        else if(sentenceIndex == 9)
        {
            scrollUI.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
