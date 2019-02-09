using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour 
{
    Scene currentScene;
    Texture2D currentCursor;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level Building" || currentScene.name == "JF_New")
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/DefaultIcon_White"), new Vector2(0, 0), CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/DefaultIcon"), new Vector2(0, 0), CursorMode.ForceSoftware);
        }
    }
}
