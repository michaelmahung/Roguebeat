using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager: MonoBehaviour
{
    [SerializeField]
    private readonly string[] AllScenes = new string[] { "MM_Spawning", "Main Menu" };

    [SerializeField]
    private Slider loadSlider;

    public void Load(string scene)
    {
        StartCoroutine(UpdateLoadBar(scene));
    }

    IEnumerator UpdateLoadBar(string scene)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(scene);

        while (!loadScene.isDone)
        {
            float trueProgress = Mathf.Clamp01(loadScene.progress / 0.9f);
            loadSlider.value = trueProgress;
            //Debug.Log(trueProgress);

            yield return null; 
        }

    }
}
