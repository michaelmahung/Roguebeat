using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour, ITrackRooms
{

    public bool IsSpawning;
    //public bool SpawnerTransition;
    //public float SpawnMovementSpeed;
    //public bool SpawnMover;

    //public Transform StartPosition;
    //public Transform EndPosition;
    public GameObject[] EnemyTypes;
    public PooledObject[] SpawnPool;
    public GameObject SpawnPoint;
    public SpawnerRoomScript MyRoomBehaviour;
    public RoomSetter MyRoom { get; set; }
    private int RandomChance; // Spawning chance of enemy types
    private float RandomSpawnTime; // small random timer for rooms to spawn enemies at different times
    [Tooltip("Mininum time to wait to be spawned")]
    public float MinSpawnRangeTime;
    [Tooltip("Maximum time to wait to be spawned")]
    public float MaxSpawnRangeTime;

    protected TagManager Tags;
    WaitForSeconds SpawnWait;



    // Use this for initialization
    void Start()
    {
        //Subscribe to these events, spawners want to know when the player enters their room and when the player dies.
        //RoomSetter.UpdatePlayerRoom += CheckPlayerRoom; Now handled by SpawnerRoomScript
        PlayerHealth.PlayerKilled += StopSpawns;
        Tags = GameManager.Instance.Tags;
        //SpawnMover = false;
        //EnemyTypes = Resources.LoadAll<GameObject>("Prefabs/Enemies"); //Done through SpawnPool
    }

    // Update is called once per frame
    //void Update()
    //{
    /*float step = SpawnMovementSpeed * Time.deltaTime;
        if (SpawnMover == false) {
            transform.position = Vector3.MoveTowards(transform.position, EndPosition.position, step);

            //Since floats are never really equal to each other I just said, if the distance between the two floats is small.
            //Was running into issues where the spawner would stop on one end or the other.
            if (Vector3.Distance(transform.position, EndPosition.position) < 0.01f) {

                SpawnMover = true;
            }
        }

        if (SpawnMover == true) {
            transform.position = Vector3.MoveTowards(transform.position, StartPosition.position, step);
            if (Vector3.Distance(transform.position, StartPosition.position) < 0.01f) {
                SpawnMover = false;
            }
        }
        */
    // }

    IEnumerator BeginSpawning()
    {
        RandomSpawnTime = Random.Range(MinSpawnRangeTime, MaxSpawnRangeTime);
        SpawnWait = new WaitForSeconds(RandomSpawnTime);
        yield return (SpawnWait);

        if (MyRoomBehaviour.EnemiesCapped() == false)
        {
            RandomChance = Random.Range(1, 100);
            if (RandomChance <= 40)
            {
                if (!gameObject.CompareTag("Spawner"))
                { // For Crane Spawning Room- Will work on later- Sam

                    //Caching the object spawned so we can tell it what room to assign itself to - Mike
                    //This should fix the issue where enemies are spawning across rooms and not activating properly

                    //GameObject go = Instantiate(EnemyTypes[2], transform.position, transform.rotation);
                    // ITrackRooms room = go.GetComponent<ITrackRooms>();
                    // room.MyRoom = MyRoom;
                }
                else if (gameObject.CompareTag("Spawner"))
                {
                    GameObject enemy = GenericPooler.Instance.GrabPrefab(SpawnPool[0]);
                    enemy.transform.position = SpawnPoint.transform.position;
                    enemy.transform.rotation = SpawnPoint.transform.rotation;

                    AI enemyBrain = enemy.GetComponent<AI>();

                    if (enemyBrain != null)
                    {
                        enemyBrain.MyRoom = MyRoom;
                        enemyBrain.SpawnerRoom = MyRoomBehaviour;
                    }

                    //ITrackRooms room = enemy.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;

                    enemy.SetActive(true);

                    //GameObject go = Instantiate(EnemyTypes[2], SpawnPoint.transform.position, transform.rotation);
                    //ITrackRooms room = go.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;
                    //Debug.Log("found it");
                }
            } 
            else if (RandomChance > 40 && RandomChance < 70)
            {
                if (!gameObject.CompareTag("Spawner"))
                { // For Crane Spawning Room- Will work on later- Sam
                  // GameObject go = Instantiate(EnemyTypes[1], transform.position, transform.rotation);
                  // ITrackRooms room = go.GetComponent<ITrackRooms>();
                  // room.MyRoom = MyRoom;
                }
                else if (gameObject.CompareTag("Spawner"))
                {
                    GameObject enemy = GenericPooler.Instance.GrabPrefab(SpawnPool[1]);
                    enemy.transform.position = SpawnPoint.transform.position;
                    enemy.transform.rotation = SpawnPoint.transform.rotation;

                    AI enemyBrain = enemy.GetComponent<AI>();

                    if (enemyBrain != null)
                    {
                        enemyBrain.MyRoom = MyRoom;
                        enemyBrain.SpawnerRoom = MyRoomBehaviour;
                    }

                    //ITrackRooms room = enemy.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;

                    enemy.SetActive(true);

                    //GameObject go = Instantiate(EnemyTypes[1], SpawnPoint.transform.position, transform.rotation);
                    //ITrackRooms room = go.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;
                }
            }
            else if (RandomChance >= 70)
            {
                if (!gameObject.CompareTag("Spawner"))
                { // For Crane Spawning Room- Will work on later- Sam
                  //GameObject go = Instantiate(EnemyTypes[0], transform.position, transform.rotation);
                  // ITrackRooms room = go.GetComponent<ITrackRooms>();
                  // room.MyRoom = MyRoom;
                }
                else if (gameObject.CompareTag("Spawner"))
                {
                    GameObject enemy = GenericPooler.Instance.GrabPrefab(SpawnPool[2]);
                    enemy.transform.position = SpawnPoint.transform.position;
                    enemy.transform.rotation = SpawnPoint.transform.rotation;

                    AI enemyBrain = enemy.GetComponent<AI>();

                    if (enemyBrain != null)
                    {
                        enemyBrain.MyRoom = MyRoom;
                        enemyBrain.SpawnerRoom = MyRoomBehaviour;
                    }

                    //ITrackRooms room = enemy.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;

                    enemy.SetActive(true);

                    //GameObject go = Instantiate(EnemyTypes[0], SpawnPoint.transform.position, transform.rotation);
                    //ITrackRooms room = go.GetComponent<ITrackRooms>();
                    //room.MyRoom = MyRoom;

                }
            }

            StartCoroutine(BeginSpawning());
            MyRoomBehaviour.AddEnemy();
        }
        else //Enemies are capped
        {
            StartCoroutine(BeginSpawning());
        }
    }
    public void StartSpawns()
    {
        IsSpawning = true;
        StartCoroutine(BeginSpawning());
    }

    public void StopSpawns()
    {
        IsSpawning = false;
        StopAllCoroutines();
    }

}
