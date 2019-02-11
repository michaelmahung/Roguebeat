using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager: MonoBehaviour
{
    [SerializeField]
    private readonly string[] AllScenes = new string[] { "Level Building", "Main Menu" };

    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
