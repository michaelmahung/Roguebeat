﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //This will become a singleton that will hold all logic and variables for the game.
    private static GameManager _instance;

    //Creating a singleton instance
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) //If there is no prexisting gamemanager in the scene
            {
                //Create a gamemanager and add base components to it. Doing this will allow for minimal gameplay with little effort.
                Debug.Log("No GameManager Instance Found, Creating Temporary GameManager Instance");
                Debug.LogError("CREATING A GAMEMANAGER FROM SCRATCH WILL CAUSE ERRORS, PLEASE ONLY USE FOR TESTING!");
                GameObject gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
                gm.AddComponent<AudioPeer>();
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
    [Header("Housekeeping")]
    [Tooltip("The current level score")]
    public float levelScore;
    [Tooltip("The current music multiplier")]
    public float musicMultipier;
    [Tooltip("The enemy base multiplier")]
    public float enemyBaseMultiplier;
    public bool gamePaused;
    [Header("High Score List")]
    [Tooltip("A list of high scores")]
    public List<float> highScores = new List<float>();
    [Header("Materials Array")]
    [Tooltip("An array of materials that can be picked from")]
    public Material [] playerMaterials;
    private GameObject player;
    private int matValue;
    private int songValue;
    [Header("Global Script References")]
    [Tooltip("Insert Reference to UIController Script")]
    public UIController UI;

    private void Awake()
    {
        _instance = this; //Make sure that this is the only active instance of the gamemanager
        playerMaterials = Resources.LoadAll<Material>("Materials"); //Load materials form our folder
        allPlayableSongs = Resources.LoadAll<AudioClip>("Music"); //Load audioclips from our folder
        player = GameObject.FindGameObjectWithTag("Player"); //Assign the gameobject based on the player tag
        DontDestroyOnLoad(this.gameObject); //Keep this object between scenes

        if (audioPlayer == null) //If there is no assigned to this gamemanager
        {
            audioPlayer = GetComponent<AudioSource>(); //Use the audiosource created by this gamemanager
            audioPlayer.clip = allPlayableSongs[0]; //Set the current clip to the first one loaded
            audioPlayer.loop = true; //Make sure we are looping tracks
            audioPlayer.Play(); //And play it
        }

        currentSong = audioPlayer.clip; //Set the current song to the one attached to the audioPlayer

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }
    }

    void Start ()
    {
        if (WeaponSwitching.Instance == null) //If there is no weaponswitching instance found
        {
            //Throw a warning
            Debug.LogWarning("Could not find an instance of WeaponSwitching class on the Player's WeaponHolder, adding one...");
            try
            {
                //Check if there is a weaponHolder in the scene
                GameObject weaponHolder = GameObject.Find("WeaponHolder");
                //If there is, add a weaponswitching component to it
                weaponHolder.AddComponent<WeaponSwitching>();
            }
            catch
            {
                //If no weaponholder is found in the scene, throw an error
                Debug.LogError("Could not find a WeaponHolder gameobject, please make sure a player ---> WeaponHolder is in the scene.");
            }

        }

        songValue = 0;
        matValue = 0;
    }

	void Update ()
    {
        //When "1" is pressed. go to the next material in the playerMaterials array.
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

        //When "2" is pressed, play the next song in the song array.
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

        //When "P" is pressed, pause the game.
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }


    //**Global Functions**


    //Pause the game
    public void PauseGame()
    {
        //Attempt to pause the game
        try
        {
            if (!gamePaused) //f the game isnt already paused, pause it
            {
                gamePaused = true;
                UI.pauseScreen.SetActive(true);
                Time.timeScale = 0;
                audioPlayer.GetComponent<AudioLowPassFilter>().enabled = true; //Enable our low pass filter
                //If no filter is attached, this will throw an error.
            }
            else
            {   //If the game is already paused, do the opposite.
                UI.pauseScreen.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
                audioPlayer.GetComponent<AudioLowPassFilter>().enabled = false;
            }
        }
        catch
        {
            //Pausing will not work without a full set GameManager, throw a warning.
            gamePaused = false;
            Debug.LogWarning("Cannot pause with a temporary GameManager.");
        }

    }

    //Can change the material on any gameobject
    public static void ChangeMaterial(GameObject go, Material mat)
    {
        go.GetComponent<Renderer>().material = mat;
    }

    //Can change the current song to any song
    public static void ChangeSong (AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
