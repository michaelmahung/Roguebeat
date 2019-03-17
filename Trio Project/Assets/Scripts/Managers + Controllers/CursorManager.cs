using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour 
{
    Vector2 cursorHotspot;
    [SerializeField] Texture2D cursorTeture;
    void Awake()
    {
        cursorHotspot = new Vector2(cursorTeture.width / 2, cursorTeture.height / 2);

        Cursor.SetCursor(cursorTeture, cursorHotspot, CursorMode.Auto);
    }
}
