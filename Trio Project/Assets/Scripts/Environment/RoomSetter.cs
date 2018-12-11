using UnityEngine;

//Basic room script for collision detection

//A delegate works very similarly to any other data type, except for the fact that it takes in functions as variables.
//If I want to make a new array I do: GameObject[] GOArray = new GameObject[];
//If I want to make a new delegate I do: PlayerEnteredNewRoom playerEntererRoom = new PlayerEnteredNewRoom(InsertFunctionToCallHere);

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
    public delegate void UpdateRoomDelegate();
    public static event UpdateRoomDelegate UpdatePlayerRoom;

	void Start () {
		if (string.IsNullOrEmpty(RoomName))
        {
            RoomName = gameObject.name;
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        ITrackRooms roomTracker = other.GetComponent<ITrackRooms>();

        if (roomTracker != null)
        {
            roomTracker.CurrentRoom = RoomName;
        }

        if (other.tag == "Player")
        {
            //If the player is found entering a new room, Update everyone listening thats listening for that event. 
            UpdatePlayer();
        }
    }

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoom = RoomName;
        UpdatePlayerRoom();
    }
}
