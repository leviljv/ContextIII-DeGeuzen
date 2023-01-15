using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInfoContainer : MonoBehaviour
{
    
    public GameObject tutInfoGameObject1, tutInfoGameObject2, tutInfoGameObject3, tutInfoGameObject4;
    public enum tutorialInfoLists
    {
        tutorialInfo1,
        tutorialInfo2,
        tutorialInfo3,
        tutorialInfo4
    }
    public int tutInfoIndex = 0;

    public tutorialInfoLists currentInfoList = tutorialInfoLists.tutorialInfo1;
    private void Start()
    {
        tutInfoGameObject2.SetActive(false);
        tutInfoGameObject3.SetActive(false); 
        tutInfoGameObject4.SetActive(false);
    }
    private void Update()
    {

        /* IM SO SORRY THIS IS AWFUL BUT I WAS CRUNCHING*/
        /* it works so deal with it bye*/
        switch (currentInfoList)
        {
            case tutorialInfoLists.tutorialInfo1:
                tutInfoGameObject1.SetActive(true);
                break;
            case tutorialInfoLists.tutorialInfo2:
                tutInfoGameObject2.SetActive(true);
                tutInfoGameObject1.SetActive(false);
                break;
            case tutorialInfoLists.tutorialInfo3:
                tutInfoGameObject2.SetActive(false);
                tutInfoGameObject3.SetActive(true);

                break;
            case tutorialInfoLists.tutorialInfo4:
                tutInfoGameObject3.SetActive(false);
                tutInfoGameObject4.SetActive(true);
                break;
        }

        
    }
    public void CycleCurrentTutorialInfo()
    {
        int toInteger = (int)currentInfoList;
        toInteger++;
        toInteger = Mathf.Clamp(toInteger, 0, 3);
        currentInfoList = (tutorialInfoLists)toInteger;
        
    }    
}
