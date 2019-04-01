using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGenerator : MonoBehaviour
{
    public delegate void NewLevel();
    public static NewLevel LoadingNextLevel;

    [SerializeField] GameObject spawnerPrefab;
    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.SaveCurrentScore();
        LoadingNextLevel();
        SceneManager.LoadScene(scene.name);
    }
}
