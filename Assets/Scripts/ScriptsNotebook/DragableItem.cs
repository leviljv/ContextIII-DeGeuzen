using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Clue clue;
    public Transform parentAfterDrag;
    public Transform backupParent;
    public Canvas CanvasTransform;
    public bool posSet = false;
    public RaycastResult raycastResult;

    public string destinationTag = "DropArea";
   
    private Vector3 offset;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private WinningCondition winningCondition;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        winningCondition = GetComponentInParent<WinningCondition>();
    }

    

    void Awake() {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        CanvasTransform = gameObject.GetComponentInParent(typeof(Canvas)) as Canvas;
        parentAfterDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(CanvasTransform.transform);
        transform.SetAsLastSibling();

        if (winningCondition.CorrectAnswers.Contains(gameObject.name)) {
            winningCondition.CorrectAnswers.Remove(gameObject.name);
            Debug.Log("IK ZAT ER AL IN");
        }
        else { 
            Debug.Log("IK ZAT ER NOG NIET IN");
        }
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        offset = transform.position - Input.mousePosition;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;       
    }

    public void OnPointerUp(PointerEventData eventData) {
        raycastResult = eventData.pointerCurrentRaycast;

        if (raycastResult.gameObject?.tag == destinationTag) {
            transform.position = raycastResult.gameObject.transform.position;
            transform.SetParent(raycastResult.gameObject.transform);

            //Debug.Log(clue.ClueAntwoord + " " + clue.ClueVraag);

            if(raycastResult.gameObject.GetComponentInParent<InventorySlot>().ClueAntwoord == clue.ClueAntwoord) {
                Debug.Log("IS GOED????");
                winningCondition.CorrectAnswers.Add(gameObject.name);
                Debug.Log(winningCondition.CorrectAnswers);
            }
            else if (raycastResult.gameObject.GetComponentInParent<InventorySlot>().ClueAntwoord != clue.ClueAntwoord) {
                Debug.Log("IS NIET GOED!!!");
                winningCondition.CorrectAnswers.Remove(gameObject.name);
            }
        }
        else {
            transform.SetParent(raycastResult.gameObject.transform);

            RemoveWrongAnswer();
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void RemoveWrongAnswer() {
        winningCondition.CorrectAnswers.Remove(gameObject.name);
        Debug.Log("NOT GOOD");
    }
}