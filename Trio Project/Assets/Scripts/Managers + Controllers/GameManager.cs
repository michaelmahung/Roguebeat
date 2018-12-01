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

    public bool isDead;

    [Header("Global Audio Information")]
    [Tooltip("Array of songs that can be played")]
    public AudioClip[] allPlayableSongs;
    [Tooltip("The current song being played")]
    public AudioClip currentSong;
    [Tooltip("The audio player we want to manipulate")]
    public AudioSource audioPlayer;

    [Header("Housekeeping")]
    public bool gamePaused;

    [Header("High Score List")]
    public List<float> highScores = new List<float>();

    [Header("Global Script References")]
    [Tooltip("Insert Reference to UIController Script")]
    public UIController UI;
    public GameObject player;

    private AudioLowPassFilter filter;
    private Material[] playerMaterials;
    Renderer playerRenderer;
    int matValue;
    int songValue;


    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        SetComponents();

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }
    }

    void Start ()
    {
        if (WeaponSwitching.Instance == null)
        {
            try
            {
                GameObject weaponHolder = GameObject.Find("WeaponHolder");
                weaponHolder.AddComponent<WeaponSwitching>();
            }
            catch
            {
                Debug.LogError("Could not find a WeaponHolder gameobject, please make sure a player ---> WeaponHolder is in the scene.");
            }

        }
    }

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (matValue < playerMaterials.Length - 1)
            {
                matValue++;
            } else
            {
                matValue = 0;
            }
            ChangeMaterial(playerRenderer, playerMaterials[matValue]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (songValue < allPlayableSongs.Length - 1)
            {
                songValue++;
            }else
            {
                songValue = 0;
            }
            ChangeSong(audioPlayer, allPlayableSongs[songValue]);
            currentSong = audioPlayer.clip;
            IChangeSong changeSong = UI.GetComponent<IChangeSong>();
            changeSong.SongChanged();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        try
        {
            if (!gamePaused)
            {
                gamePaused = true;
                UI.pauseScreen.SetActive(true);
                Time.timeScale = 0;
                filter.enabled = true;
            }
            else
            {   
                UI.pauseScreen.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
                filter.enabled = false;
            }
        }
        catch
        {
            gamePaused = false;
            Debug.LogWarning("Add a reference to the UIController in the GameManager to pause.");
        }

    }

    public void ChangeMaterial(Renderer renderer, Material mat)
    {
        renderer.material = mat;
    }

    public void ChangeSong (AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    private void SetComponents()
    {
        playerMaterials = Resources.LoadAll<Material>("Materials");
        allPlayableSongs = Resources.LoadAll<AudioClip>("Music");

        player = FindObjectOfType<PlayerHealth>().gameObject;
        playerRenderer = player.GetComponentInChildren<Renderer>();

        filter = audioPlayer.GetComponent<AudioLowPassFilter>();
        filter.lowpassResonanceQ = 400;
        currentSong = audioPlayer.clip;

        songValue = 0;
        matValue = 0;
    }

}
