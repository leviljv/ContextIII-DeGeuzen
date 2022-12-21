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
    private WinningCondition winningCondition;
    Vector3 offset;
    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";
    float x;
    float y;
    float z;
    Vector3 pos;
    RectTransform rectTransform;
    public bool posSet = false;
    //public Vector3 newPos;
    Vector3 Position;
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
        if (winningCondition.CorrectAnswers.Contains(gameObject.GetComponent<ClueAnswer>().ClueScriptableObject))
        {
            winningCondition.CorrectAnswers.Remove(gameObject.GetComponent<ClueAnswer>().ClueScriptableObject);
            Debug.Log("IK ZAT ER AL IN");
        }
        else
        {
            Debug.Log("IK ZAT ER NOG NIET IN");
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Input.mousePosition;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //gameObject.transform.position = newPos;
        raycastResult = eventData.pointerCurrentRaycast;
        if (raycastResult.gameObject?.tag == destinationTag)
        {

            transform.position = raycastResult.gameObject.transform.position;
            transform.SetParent(raycastResult.gameObject.transform);
            //if(raycastResult.gameObject.transform == parentAfterDrag)
            //{
            //    Debug.Log("SUCCES");
            //    winningCondition.CorrectAnswers.Add(parentAfterDrag.gameObject);
            //}
            //else if(raycastResult.gameObject.transform != parentAfterDrag)
            //{
            //    Debug.Log("NOT GOOD");
            //    winningCondition.CorrectAnswers.Remove(parentAfterDrag.gameObject);
            //}
            Debug.Log(clue.ClueAntwoord + " " + clue.ClueVraag);
            if(raycastResult.gameObject.GetComponent<InventorySlot>().Antwoord.text == clue.ClueAntwoord)
            {
                Debug.Log("IS GOED????");
                winningCondition.CorrectAnswers.Add(gameObject.GetComponent<ClueAnswer>().ClueScriptableObject);
                Debug.Log(winningCondition.CorrectAnswers);
            }
            else if (raycastResult.gameObject.GetComponent<InventorySlot>().Antwoord.text != clue.ClueAntwoord)
            {
                Debug.Log("IS NIET GOED!!!");
                winningCondition.CorrectAnswers.Remove(gameObject.GetComponent<ClueAnswer>().ClueScriptableObject);
            }
        }
        else
        {
            transform.SetParent(raycastResult.gameObject.transform);

            test();
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void test()
    {
        winningCondition.CorrectAnswers.Remove(gameObject.GetComponent<ClueAnswer>().ClueScriptableObject);
        Debug.Log("NOT GOOD");
    }
}
