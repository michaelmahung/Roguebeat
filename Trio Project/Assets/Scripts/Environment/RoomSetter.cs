using UnityEngine;

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

public class RoomSetter : MonoBehaviour {

    public string RoomName;
    public BaseDoor MyDoor;
    public SpawnEnemies[] MySpawners;

    public Transform camPlacement;
    public GameObject cam;

    private CameraController2 cc;

    private int EnemyCount;
    [SerializeField]
    private int EnemyCap;

    public delegate void UpdateRoomDelegate();
    public static event UpdateRoomDelegate UpdatePlayerRoom;

    private void Awake()
    {
        MyDoor = GetComponentInChildren<BaseDoor>();
        MySpawners = GetComponentsInChildren<SpawnEnemies>();
        if (MyDoor != null)
        {
            MyDoor.MyRoom = this;
        }
    }

    void Start () {
		if (string.IsNullOrEmpty(RoomName))
        {
            RoomName = gameObject.name;
            cc = GameObject.FindObjectOfType<CameraController2>();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        ITrackRooms roomTracker = other.GetComponent<ITrackRooms>();

        if (roomTracker != null)
        {
            roomTracker.MyRoomName = RoomName;
            if (roomTracker.MyRoom == null)
            {
                roomTracker.MyRoom = this;
            }
        }

        if (other.tag == "Player")
        {
            cc.player = camPlacement.gameObject;
        
            //If the player is found entering a new room, Update everyone listening thats listening for that event. 
            UpdatePlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UpdatePlayerRoom();
    }

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoom = RoomName;
        UpdatePlayerRoom();
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
}
