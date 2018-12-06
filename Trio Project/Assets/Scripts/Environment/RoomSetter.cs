using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetter : MonoBehaviour {

    public string RoomName;
    public delegate void PlayerEnteredNewRoom();
    public static event PlayerEnteredNewRoom UpdatePlayerRoom;

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
            UpdatePlayer();
        }
    }

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoom = RoomName;
        UpdatePlayerRoom();
    }
}
