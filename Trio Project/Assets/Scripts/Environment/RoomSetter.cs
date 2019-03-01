using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Basic room script for collision detection

//A delegate works very similarly to any other data type, except for the fact that it takes in functions as variables.
//If I want to make a new array I do: GameObject[] GOArray = new GameObject[];
//If I want to make a new delegate I do: PlayerEnteredNewRoom playerEnteredRoom = new PlayerEnteredNewRoom(InsertFunctionToCallHere);

//One thing to note with delegates is that the return type is important to keep consistent.
//Because I made a delegate void - it can only interact with other void functions.

/*
 * For Example:
 * 
 * public string ReturnString(String enterstring)
 * {
 *      //This would not work with the delegate
 * }
 * 
 * public void ReturnString()
 * {
 *      //This would work with the delegate
 * }
 * 
 * */

//Ok so why bother with this overly complicated data type?
//The benefit from this is that we can create a static event - making it accessable to any other script.
//In this case we will make a global event called UpdatePlayerRoom that will alert any script that wants to listen to it whenever the player enters a new room.

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

    public delegate void UpdateRoomDelegate();
    public static event UpdateRoomDelegate UpdatePlayerRoom;

    void Awake()
    {
        LevelSpawning.FinishedSpawningRooms += FinalizeRoom;
        camController = FindObjectOfType<CameraController2>();

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

        if (roomTracker != null && other.tag != "Enemy")
        {
            roomTracker.MyRoomName = RoomName;

            if (roomTracker.MyRoom == null)
            {
                //Debug.Log(other.gameObject.name);
                roomTracker.MyRoom = this;
            }
        }

        if (other.tag == "Player")
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

    /*private void OnTriggerExit(Collider other)
    {
        if (UpdatePlayerRoom != null)
        {
            UpdatePlayerRoom();
        }

        if(other.tag == "Player"){

            if (myLight != null)
            {
                myLight.ToggleLight(false);
            }
        }
    }*/

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoomName = RoomName;
        GameManager.Instance.PlayerRoom = this;

        if (UpdatePlayerRoom != null)
        {
            UpdatePlayerRoom();
        }
    }

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
