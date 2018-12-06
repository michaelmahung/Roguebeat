using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetter : MonoBehaviour {

    public string RoomName;

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
    }

}
