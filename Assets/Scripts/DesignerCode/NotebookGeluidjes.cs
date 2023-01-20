using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookGeluidjes : MonoBehaviour
{
    public AudioSource bookOpen;
    public AudioSource bookClosed;
    bool openPlay = false;
    bool closePlay = false;

    bool showedBook = false;
 

    // Update is called once per frame
    void Update()
    {
       
       

    }

    IEnumerator kys(bool canPlay)
    {
        canPlay = true;
        yield return new WaitForSeconds(1);
        canPlay = false;

    }
}
