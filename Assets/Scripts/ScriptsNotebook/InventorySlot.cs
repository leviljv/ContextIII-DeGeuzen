using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Clue ClueScriptableObject;
    public TMP_Text ClueVraag;
    public TMP_Text Antwoord;
    public Image taart;

    private void Start()
    {
        ClueVraag.text = ClueScriptableObject.ClueVraag;
        Antwoord.text = ClueScriptableObject.ClueAntwoord;
        taart.gameObject.SetActive(false);
    }

    public void ToggleActive()
    {
        Debug.Log("Doe ik het?" + taart.gameObject.name);
        taart.gameObject.SetActive(false);
    }


    public void OnDrop(PointerEventData eventData)
    {
        //GameObject dropped = eventData.pointerDrag;
        //DragableItem dragableItem = dropped.GetComponent<DragableItem>();
        //dragableItem.parentAfterDrag = transform;
    }

    
}
