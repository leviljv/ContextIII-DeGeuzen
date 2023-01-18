using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D handOpen;
    public Texture2D handWijs;
    public Texture2D handHold;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(handWijs, hotSpot, cursorMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(handOpen, Vector2.zero, CursorMode.Auto);
    }
}
