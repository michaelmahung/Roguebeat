using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //This will become a singleton that will hold all logic and variables for the game.
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Creating GameManager Instance");
                GameObject gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    //Use space here to declare variables.
    //public enum Weapons {Laser, Blaster, MerryGoRound, Hammer, Flamethrower, Last };
    [Tooltip("Is the player dead?")]
    public bool isDead;
    [Header("Global Audio Information")]
    [Tooltip("Array of songs that can be played")]
    public AudioClip[] allPlayableSongs;
    [Tooltip("The current song being played")]
    public AudioClip currentSong;
    [Tooltip("The audio player we want to manipulate")]
    public AudioSource audioPlayer;
    [Header("Weapons")]
    [Tooltip("Array of usable weapons")]
    public GameObject[] allWeapons;
    [Tooltip("The players current weapon")]
    public GameObject currentWeapon;
    [Header("Housekeeping")]
    [Tooltip("The current level score")]
    public float levelScore;
    [Tooltip("The current music multiplier")]
    public float musicMultipier;
    [Tooltip("The enemy base multiplier")]
    public float enemyBaseMultiplier;
    [Header("High Score List")]
    [Tooltip("A list of high scores")]
    public List<float> highScores = new List<float>();
    [Header("Materials Array")]
    [Tooltip("An array of materials that can be picked from")]
    public Material [] playerMaterials;
    private GameObject player;
    private int weaponValue;
    private int matValue;
    private int songValue;
    private bool gamePaused;
    [Header("Global Script References")]
    [Tooltip("Insert Reference to UIController Script")]
    public UIController UI;

    private void Awake()
    {
        player = GameObject.Find("Player");
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }
    }

    void Start ()
    {
        allWeapons = Resources.LoadAll<GameObject>("Weapons");
        playerMaterials = Resources.LoadAll<Material>("Materials");
        allPlayableSongs = Resources.LoadAll<AudioClip>("Music");
        currentWeapon = allWeapons[0];
        songValue = 0;
        matValue = 0;
        currentSong = audioPlayer.clip;
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
            ChangeMaterial(player, playerMaterials[matValue]);
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
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (weaponValue < allWeapons.Length - 1)
                {
                    weaponValue++;
                }
                else
                {
                    weaponValue = 0;
                }
                currentWeapon = allWeapons[weaponValue];
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }


    //Global Functions

    public void PauseGame()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            UI.pauseScreen.SetActive(true);
            Time.timeScale = 0;
            audioPlayer.GetComponent<AudioLowPassFilter>().enabled = true;
        } else 
        {
            UI.pauseScreen.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1;
            audioPlayer.GetComponent<AudioLowPassFilter>().enabled = false;
        }
    }

    public static void ChangeMaterial(GameObject go, Material mat)
    {
        go.GetComponent<Renderer>().material = mat;
    }

    public static void ChangeSong (AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
