using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager: MonoBehaviour
{
    [SerializeField]
    private readonly string[] AllScenes = new string[] { "MM_Spawning", "Main Menu" };

    public void Load(string scene)
    {
        //SceneManager.LoadScene(scene);
        SceneManager.LoadSceneAsync(scene);
    }

}
