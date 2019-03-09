using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//TODO clean up or split this class up - maybe rename?

public class RoomSetter : MonoBehaviour {

    public int EnemyCount;
    public int EnemyCap;
    public string RoomName;
    public List<BaseDoor> MyDoors = new List<BaseDoor>();

    public bool IsCleared { get; private set; }

    public SpawnEnemies[] MySpawners;
    public RoomSpawnPoint[] MyOpenWalls;
    [SerializeField] private Transform camPlacement;
    [SerializeField] private GameObject cam;

    private CameraController2 camController;
    private RoomLight myLight;
    TagManager Tags;

    public delegate void UpdateRoomDelegate();
    public static event UpdateRoomDelegate UpdatePlayerRoom;

    void Awake()
    {
        LevelSpawning.FinishedSpawningRooms += FinalizeRoom;
        camController = FindObjectOfType<CameraController2>();

        Tags = GameManager.Instance.Tags;
        cam = camController.gameObject;
    }

    void Start () {

        PlayerHealth.PlayerKilled += OpenDoors;

        if (string.IsNullOrEmpty(RoomName))
        {
            RoomName = gameObject.name;
        }
    }

    void FinalizeRoom()
    {
        LevelSpawning.FinishedSpawningRooms -= FinalizeRoom;
        MyOpenWalls = GetComponentsInChildren<RoomSpawnPoint>();

        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            point.SetMyRoom(this);
        }

        FindComponents();
    }

    private IEnumerator CloseDoors()
    {
        yield return new WaitForSeconds(1f);

        foreach (BaseDoor door in MyDoors)
        {
            door.CloseDoor();
        }

        yield break;
    }

    private void OpenDoors()
    {
        foreach(BaseDoor door in MyDoors)
        {
            if (door.DoorMoved)
            door.OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ITrackRooms roomTracker = other.GetComponent<ITrackRooms>();
        //Debug.Log("My room is: " + RoomName);

        if (roomTracker != null && !other.CompareTag(Tags.EnemyTag))
        {
            roomTracker.MyRoomName = RoomName;

            if (roomTracker.MyRoom == null)
            {
                //Debug.Log(other.gameObject.name);
                roomTracker.MyRoom = this;
            }
        }

        if (other.CompareTag(Tags.PlayerTag))
        {
            camController.SetFocalPoint(camPlacement.gameObject);

            if (myLight != null)
            {
                myLight.ToggleLight(true);
            }

            //If the player is found entering a new room, Update everyone listening thats listening for that event. 
            UpdatePlayer();
            StartCoroutine(CloseDoors());
        }
    }

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoomName = RoomName;
        GameManager.Instance.PlayerRoom = this;
        //Debug.Log("Player room is: " + GameManager.Instance.PlayerRoom);

        if (UpdatePlayerRoom != null)
        {
            UpdatePlayerRoom();
        }
    }


    //TODO -- move this to spawner room logic
    public void AddEnemy()
    {
        EnemyCount++;
    }

    public void RemoveEnemy()
    {
        EnemyCount--;
    }

    public bool EnemiesCapped()
    {
        if (EnemyCount >= EnemyCap)
        {
            return true;
        }
        return false;
    }

    public void RoomCleared()
    {
        IsCleared = true;
    }

    void FindComponents()
    {
        BaseDoor[] _doors = GetComponentsInChildren<BaseDoor>();

        foreach(BaseDoor baseDoor in _doors)
        {
            MyDoors.Add(baseDoor);
        }

        MySpawners = GetComponentsInChildren<SpawnEnemies>();
        myLight = GetComponentInChildren<RoomLight>();
        MyOpenWalls = GetComponentsInChildren<RoomSpawnPoint>();

        if (camPlacement == null)
        {
            camPlacement = GameManager.Instance.PlayerObject.transform;
        }

        if (cam == null)
        {
            cam = FindObjectOfType<CameraController2>().gameObject;
        }

        if (MyDoors.Count > 0)
        {
            foreach (BaseDoor door in MyDoors)
            {
                door.AddRoom(this);
            }
        }

        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            point.SpawnRandom();
        }
    }
}
