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
                Debug.LogError("No GameManager in scene");
                GameObject gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    [Header("Scorekeeping")]
    [SerializeField] private int _currentScore;
    public int CurrentScore { get { return _currentScore; } set { _currentScore = value; } }

    [Header("High Score List")]
    [SerializeField] private List<float> HighScores = new List<float>();

    [Header("Global Script References")]
    public RoomSetter PlayerRoom;
    public UIController UI;
    public Vector3 PlayerSpawnPosition;
    public CameraShake CameraShaker;
    public WeaponAudio WeaponSounds;
    public PlayerHealth PlayerHealthRef;
    public bool IsPlayerDead { get { return PlayerHealthRef.IsPlayerDead; } }
    public GameObject PlayerObject { get; private set; }
    public TagManager Tags = new TagManager();

    [Header("Difficulty Settings")]
    //Difficulty Multiplier work by Sam
    private readonly float[] multiplier = { 1.0f, 1.25f, 1.5f, 1.75f };
    public float Difficulty;

    [Header("Misc")]
    [SerializeField] CameraBlocker camBlock;

    public delegate void OnScoreAdded();
    public event OnScoreAdded ScoreAdded;
    public delegate void OnPlayerRespawn();
    public event OnPlayerRespawn PlayerRespawned;

    public PlayerMovement PlayerMovementReference { get; private set; }

    private bool canRespawn;

    public void AddScore(int score)
    {
        CurrentScore += score;

        if (CurrentScore < 0)
        {
            CurrentScore = 0;
        }

        ScoreAdded(); //Tell everyone weve changed our score.
    }

    //Why not cache player rigidbody?
    public void ResetPlayerPosition()
    {
        PlayerObject.transform.position = PlayerSpawnPosition;
        Rigidbody rb = PlayerObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void RespawnPlayer()
    {
        if (canRespawn)
        {
            PlayerObject.SetActive(true);
            ResetPlayerPosition();
            PlayerRespawned();
        }
    }

    #region Private Functions
    private void Awake()
    {
        _instance = this;
        //DontDestroyOnLoad(gameObject); //TODO - make GameManager it's own object in scene.

        SetComponents();
        Difficulty = multiplier[3];

        if (PlayerPrefs.HasKey("HighScores"))
        {
            //Load saved highscore array.
        }

        LevelSpawning.FinishedSpawningRooms += FindStartLocation;
    }

    private void Start()
    {
        PlayerHealth.PlayerKilled += CanRespawn;
        

        //If there isnt a default spawn position set, make where the player starts in the scene the spawn position.
        if (PlayerSpawnPosition == Vector3.zero)
        {
            PlayerSpawnPosition = PlayerObject.transform.position;
        }
    }

    //TODO -- dont do this
    private void FindStartLocation()
    {
        PlayerSpawnPosition = GameObject.Find("PlayerSpawn").transform.position;
        ResetPlayerPosition();

        if (camBlock != null)
        {
            camBlock.Invoke("UnBlockCamera", 0.5f);
        }
    }

    //Basically the start function
    private void SetComponents()
    {
        WeaponSounds = FindObjectOfType<WeaponAudio>();
        PlayerHealthRef = FindObjectOfType<PlayerHealth>();
        PlayerObject = PlayerHealthRef.gameObject;
        PlayerMovementReference = PlayerObject.GetComponent<PlayerMovement>();
        UI = FindObjectOfType<UIController>();
    }

    private void CanRespawn()
    {
        canRespawn = true;
    }

    #endregion

}
