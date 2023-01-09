using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollectible : MonoBehaviour, IInteractable
{
    public Collectible scriptableObject;
    public NoteBookV2 noteBook;


    public void Interact()
    {
        if (noteBook.CollectibleListEntries < 6)
        {
            noteBook.collectibleManager.gameObject.transform.GetChild(noteBook.CollectibleChildNumber).GetComponent<CollectibleHolder>().CollectibleList.Add(scriptableObject);
            noteBook.CollectibleListEntries++;
        }
        else if (noteBook.CollectibleListEntries == 6)
        {
            noteBook.CollectibleChildNumber++;
            noteBook.CollectibleListEntries = 0;
        }
    }

}
