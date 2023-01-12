using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D CursorNormal;
    public Texture2D CursorPointer;
    public Vector2 NormalCursorHotspot = new Vector2(0,0);
    public Vector2 PointerCursorHotspot = new Vector2(0,0);
    
    void Start()
    {
        Cursor.SetCursor(CursorNormal, NormalCursorHotspot, CursorMode.Auto);
    }

    public void OnButtonCursorEnter()
    {
        Cursor.SetCursor(CursorPointer, PointerCursorHotspot , CursorMode.Auto);
    }

    public void OnButtonCursorExit()
    {
        Cursor.SetCursor(CursorNormal, NormalCursorHotspot, CursorMode.Auto);
    }
}
