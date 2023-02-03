using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollectible : MonoBehaviour, IInteractable
{
    //public NoteBookV2 noteBook;

    public void Interact()
    {
        if (NoteBookV2.instance.CollectiblesFound < 5)
        {
            NoteBookV2.instance.CollectiblesFound++;
            
        }
        else if (NoteBookV2.instance.CollectiblesFound == 5)
        {
            NoteBookV2.instance.aaah.Test();
            NoteBookV2.instance.CollectibleChildNumber++;
            NoteBookV2.instance.CollectiblesFound = 0;
        }
        EventManager.Invoke(EventType.COLLETABLE_FOUND);
        AudioPlaceholder.instance.GrabCoin();
        Destroy(gameObject);
    }
}