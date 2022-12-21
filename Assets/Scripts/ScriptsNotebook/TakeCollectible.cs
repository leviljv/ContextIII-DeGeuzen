using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollectible : MonoBehaviour, IInteractable
{
    public ScriptableObject scriptableObject;
    public NoteBook noteBook;

    
    
    public void Interact()
    {
        noteBook.ManageCollectibles(scriptableObject);
    }
    
}
