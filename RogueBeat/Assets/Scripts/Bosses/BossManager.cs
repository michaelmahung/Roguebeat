using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private static BossManager _instance;
    public static BossManager Instance { get { return _instance; } }

    SceneGenerator sceneGen;

    public List<BossController> MainBosses;
    BossController[] bossControllers;

    public GameObject QuitButton;
    public GameObject Instructions;
    public GameObject Instructionskey;
    public GameObject GameOverText;

    private void Awake()
    {
        _instance = this;

        LevelSpawning.FinishedSpawningRooms += FindBosses;
        SceneGenerator.LoadingNextLevel += ResetAll;
    }

    void ResetAll()
    {
        SceneGenerator.LoadingNextLevel -= ResetAll;
        LevelSpawning.FinishedSpawningRooms -= FindBosses;
    }

    private void Start()
    {
        Instructionskey.SetActive(true);
        Instructions.SetActive(false);
        GameOverText.SetActive(false);
    }

    public void RemoveBoss(BossController boss)
    {
        MainBosses.Remove(boss);
        CheckBosses();
    }

    void CheckBosses()
    {
        if (MainBosses.Count > 0)
        {
            Debug.Log("Bosses Remaining: " + MainBosses.Count);
            return;
        }

        sceneGen.LoadNextLevel();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (QuitButton.activeSelf)
            {
                Time.timeScale = 1;
                QuitButton.SetActive(false);
            }
            else if (!QuitButton.activeSelf)
            {
                QuitButton.SetActive(true);
                Time.timeScale = 0;
            }

        }

        if (Input.GetKeyDown(KeyCode.I))
        {

            if (Instructionskey.activeSelf)
            {
                Instructionskey.SetActive(false);
                Instructions.SetActive(true);
            }
            else if (!Instructionskey.activeSelf)
            {
                Instructionskey.SetActive(true);
                Instructions.SetActive(false);
            }
        }
    }

    void FindBosses()
    {
        sceneGen = FindObjectOfType<SceneGenerator>();

        bossControllers = GameObject.FindObjectsOfType<BossController>();

        MainBosses = new List<BossController>();

        foreach (BossController bc in bossControllers)
        {
            if (bc.IsMainBoss == true)
            {
                MainBosses.Add(bc);
            }
        }

        Debug.Log("Total bosses: " + MainBosses.Count);
    }
}
