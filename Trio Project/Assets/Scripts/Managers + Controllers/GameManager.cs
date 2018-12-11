using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //The GameManager class will be whats known as a "Singleton". Singletons are single instnces of classes,
    //that are used globally throughout the game. In general, singletons should be used sparingly, but in this case,
    //we need a class that can be communicated with globally to keep track of scores and such.
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            //When something trys to access the GameManager Instance, itll check if there already is an Instance running.
            //If there isn't create an empty GameObject and attach the GameManager script and an AudioPeer script.
            //If there is an Instance, simply return this instance.
            //To avoid their being multiple instances, we set _instance = this in the Awake() function.

            if (_instance == null) 
            {
                Debug.LogError("Creating a GameManager instance from scratch, this is not ideal.\nPlease add a GameManager component to the scene");
                GameObject gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
                gm.AddComponent<AudioPeer>();
            }
            return _instance;
        }
    }

    [Header("Global Audio Information")]
    [Tooltip("The current song being played")]
    public AudioClip CurrentSong;
    [Tooltip("The audio player we want to manipulate")]
    public AudioSource AudioPlayer;

    [Header("ScoreKeeping")]
    public int CurrentScore;

    [Header("High Score List")]
    public List<float> HighScores = new List<float>();

    [Header("Global Script References")]
    public GameObject Player;
    public string PlayerRoom;
    public List<BaseDoor> ActiveDoors = new List<BaseDoor>();
    public List<SpawnEnemies> ActiveSpawners = new List<SpawnEnemies>();
    public AudioLowPassFilter Filter;
    public UIController UI;

    private bool canRespawn;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        FindDoors();
        FindSpawners();
        SetComponents();

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }
    }

    void Start ()
    {
        PlayerHealth.PlayerKilled += CanRespawn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }



    /// <summary>
    /// Public Functions
    /// </summary>


    //Function for adding to the counters of the various room doors.
    public void AddToDoor(String roomName, BaseDoor.openCondition type)
    {
        for (int i = 0; i < ActiveDoors.Count; i++)
        {
            if (ActiveDoors[i].CurrentRoom == roomName && type == ActiveDoors[i].OpenCondition)
            {
                ActiveDoors[i].AddToDoor();
                break;
            }
        }
    }

    //Removes a door from the overall door list
    public void RemoveDoor(BaseDoor door)
    {
        ActiveDoors.Remove(door);
    }

    //Removes a spawner from the overall spawner list
    public void RemoveSpawner(SpawnEnemies spawn)
    {
        ActiveSpawners.Remove(spawn);
    }






    /// <summary>
    /// Private Functions
    /// </summary>

    //Basically the start function
    private void SetComponents()
    {
        Player = FindObjectOfType<PlayerHealth>().gameObject;
        PlayerRoom = Player.GetComponent<PlayerStats>().CurrentRoom;
        Filter = AudioPlayer.GetComponent<AudioLowPassFilter>();
        Filter.cutoffFrequency = 400;
        CurrentSong = AudioPlayer.clip;
        UI = GameObject.FindObjectOfType<UIController>();
    }

    private void FindSpawners()
    {
        SpawnEnemies[] spawners;
        spawners = FindObjectsOfType<SpawnEnemies>();
        foreach (SpawnEnemies spawn in spawners)
        {
            ActiveSpawners.Add(spawn);
        }
    }

    private void FindDoors()
    {
        BaseDoor[] doors;
        doors = GameObject.FindObjectsOfType<BaseDoor>();
        foreach (BaseDoor door in doors)
        {
            ActiveDoors.Add(door);
        }
    }

    private void CanRespawn()
    {
        canRespawn = true;
    }


    private void RespawnPlayer()
    {
        if (canRespawn)
        {
            SceneManager.LoadScene("Level Building");
        }
    }

}
