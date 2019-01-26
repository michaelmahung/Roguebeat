using System.Collections.Generic;
using UnityEngine;
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
            }
            return _instance;
        }
    }

    [Header("Scorekeeping")]
    public int CurrentScore;

    [Header("High Score List")]
    public List<float> HighScores = new List<float>();

    [Header("Global Script References")]
    public GameObject Player;
    public string PlayerRoom;
    public UIController UI;
    public Vector3 PlayerSpawnPosition;
    public Shaker cameraShaker;

    [Header("Difficulty Settings")]
    //Difficulty Multiplier work by Sam
    public float[] Multiplier = {1.0f, 1.25f, 1.5f, 1.75f};
    public float Difficulty;

    public delegate void OnScoreAdded();
    public event OnScoreAdded ScoreAdded;
    public delegate void OnPlayerRespawn();
    public event OnPlayerRespawn PlayerRespawned;

    private bool canRespawn;


    public void AddScore(int score)
    {
        CurrentScore += score;
        if (CurrentScore < 0)
        {
            CurrentScore = 0;
        }
        ScoreAdded();
    }

    public void ResetPlayerPosition()
    {
        Player.transform.position = PlayerSpawnPosition;
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    #region Private Functions
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        SetComponents();
        Difficulty = Multiplier[3];

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }
    }

    private void Start()
    {
        PlayerHealth.PlayerKilled += CanRespawn;

        if (PlayerSpawnPosition == null)
        {
            PlayerSpawnPosition = Player.transform.position;
        }
    }

    private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			RespawnPlayer ();
		}

		if (Input.GetKeyDown (KeyCode.V)) {
		Difficulty = Multiplier[3];
			print(Difficulty);
		}
    }

    //Basically the start function
    private void SetComponents()
    {
        Player = FindObjectOfType<PlayerHealth>().gameObject;
        PlayerRoom = Player.GetComponent<PlayerStats>().MyRoomName;
        UI = GameObject.FindObjectOfType<UIController>();
    }

    private void CanRespawn()
    {
        canRespawn = true;
    }


    private void RespawnPlayer()
    {
        if (canRespawn)
        {
            Player.SetActive(true);
            ResetPlayerPosition();
            PlayerRespawned();
        }
    }

    #endregion

}
