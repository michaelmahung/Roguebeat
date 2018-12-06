using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
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
    public AudioClip currentSong;
    [Tooltip("The audio player we want to manipulate")]
    public AudioSource audioPlayer;

    [Header("High Score List")]
    public List<float> highScores = new List<float>();

    [Header("Global Script References")]
    public GameObject Player;
    public string PlayerRoom;
    public List<BaseDoor> ActiveDoors = new List<BaseDoor>();
    public List<SpawnEnemies> ActiveSpawners = new List<SpawnEnemies>();
    public AudioLowPassFilter filter;
    public UIController UI;

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

    }

    /// <summary>
    /// Public Functions
    /// </summary>


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

    public void RemoveDoor(BaseDoor door)
    {
        ActiveDoors.Remove(door);
    }

    public void RemoveSpawner(SpawnEnemies spawn)
    {
        ActiveSpawners.Remove(spawn);
    }



    /// <summary>
    /// Private Functions
    /// </summary>

    private void SetComponents()
    {
        Player = FindObjectOfType<PlayerHealth>().gameObject;
        PlayerRoom = Player.GetComponent<PlayerStats>().CurrentRoom;
        filter = audioPlayer.GetComponent<AudioLowPassFilter>();
        filter.cutoffFrequency = 400;
        currentSong = audioPlayer.clip;
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

}
