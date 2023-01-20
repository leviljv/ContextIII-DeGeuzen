using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public ClueAnswerSO clue;
    [HideInInspector]public Transform parentAfterDrag;
    [HideInInspector]public Transform backupParent;
    [HideInInspector]public Canvas CanvasTransform;
    [HideInInspector]public bool posSet = false;
    private WinningCondition winningCondition;
    Vector3 offset;
    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";
    float x;
    float y;
    float z;
    Vector3 pos;
    RectTransform rectTransform;
    //public Vector3 newPos;
    Vector3 Position;

    
    public Texture2D handHold;
    public Texture2D handOpen;
    public Texture2D handWijs;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    public RaycastResult raycastResult;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        winningCondition = GetComponentInParent<WinningCondition>();
        //rectTransform.localPosition = newPos;

        if (posSet == false)
        {
            SetRandomPos();
        }

    }


    public void SetRandomPos()
    {
        //Debug.Log("Hyva");
        x = Random.Range(100, 200);
        y = Random.Range(-150, -300);
        z = 2;
        pos = new Vector3(x, y, z);
        rectTransform.localPosition = pos;
        posSet = true;
    }
    void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        CanvasTransform = gameObject.GetComponentInParent(typeof(Canvas)) as Canvas;
        parentAfterDrag = transform.parent;

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(CanvasTransform.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Cursor.SetCursor(handHold, hotSpot, cursorMode);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Input.mousePosition;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        if (winningCondition.index > 0 && winningCondition.CorrectGameObjects.Contains(gameObject))
        {
            winningCondition.index--;
        }
        winningCondition.CorrectAnswers.Remove(clue);
        winningCondition.CorrectGameObjects.Remove(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Cursor.SetCursor(handWijs, hotSpot, cursorMode);
        //gameObject.transform.position = newPos;
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        var tmp = raycastResult;
        if (raycastResult.gameObject?.tag == destinationTag)
        {
            transform.position = raycastResult.gameObject.transform.position;
            transform.SetParent(raycastResult.gameObject.transform);

            if (raycastResult.gameObject.GetComponent<InventorySlot>().Antwoord.text == clue.ClueAntwoord)
            {
                Debug.Log("IS GOED????");
                winningCondition.CorrectAnswers.Add(clue);
                winningCondition.CorrectGameObjects.Add(gameObject);
                winningCondition.index++;
            }
            else if (raycastResult.gameObject.GetComponent<InventorySlot>().Antwoord.text != clue.ClueAntwoord)
            {
                Debug.Log("IS NIET GOED!!!");
                if(winningCondition.index > 0  && winningCondition.CorrectGameObjects.Contains(gameObject))
                {
                    winningCondition.index--;
                }
                winningCondition.CorrectAnswers.Remove(clue);
                winningCondition.CorrectGameObjects.Remove(gameObject);
            }
        }
        else
        {
            transform.SetParent(raycastResult.gameObject.transform);

            if (winningCondition.index > 0  && winningCondition.CorrectGameObjects.Contains(gameObject))
            {
                winningCondition.index--;
            }
            winningCondition.CorrectAnswers.Remove(clue);
            winningCondition.CorrectGameObjects.Remove(gameObject);

            Debug.Log("NOT GOOD");
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}