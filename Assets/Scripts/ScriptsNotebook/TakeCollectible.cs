using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollectible : MonoBehaviour, IInteractable
{
    public NoteBookV2 noteBook;

    public void Interact()
    {
        if (noteBook.CollectiblesFound < 5)
        {
            noteBook.CollectiblesFound++;
        }
        else if (noteBook.CollectiblesFound == 5)
        {
            noteBook.aaah.Test();
            noteBook.CollectibleChildNumber++;
            noteBook.CollectiblesFound = 0;
        }
        Destroy(gameObject);
    }

}
