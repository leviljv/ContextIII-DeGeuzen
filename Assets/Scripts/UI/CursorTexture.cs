using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    public Texture2D point;
    public Texture2D grab;
    public Texture2D open;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start() {
        Cursor.SetCursor(point, hotSpot, cursorMode);
    }

    void Update() {
        
    }
}