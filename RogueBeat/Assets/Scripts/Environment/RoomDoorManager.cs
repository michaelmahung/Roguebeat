//TODO -- finish splitting roomsetter into door manager and room setter
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoorManager : MonoBehaviour
{
    public string RoomName;
    public RoomSetter MyRoom;

    public bool StartRoom;
    public bool EndRoom;
    public bool IsCleared { get; private set; }

    public List<BaseDoor> MyDoors = new List<BaseDoor>();

    void Start()
    {
        PlayerHealth.PlayerKilled += OpenClearedDoors;
    }

    public void FindComponents()
    {
        BaseDoor[] _doors = GetComponentsInChildren<BaseDoor>();

        foreach (BaseDoor baseDoor in _doors)
        {
            if (baseDoor != null)
            {
                MyDoors.Add(baseDoor);
            }
        }

        if (MyDoors.Count > 0)
        {
            foreach (BaseDoor door in MyDoors)
            {
                door.AddRoom(MyRoom);
            }
        }
    }

    public void SetSpecialDoors()
    {
        if (StartRoom || EndRoom)
        {
            foreach (RoomSpawnPoint point in MyRoom.MyOpenWalls)
            {
                if (point.OtherRoom != null)
                {
                    StartDoor door = point.GetComponentInChildren<StartDoor>();

                    if (door != null)
                    {
                        //Debug.Log("Opening door");
                        door.AddRoom(MyRoom);
                        MyDoors.Add(door);
                        point.OtherRoom.DoorManager.MyDoors.Add(door);

                        if (StartRoom)
                        {
                            door.Invoke("OpenDoor", 2);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator cDoors()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Closing Doors");

        foreach (BaseDoor door in MyDoors)
        {
            if (door != null)
            {
                door.CloseDoor();
            }
        }

        yield break;
    }

    public void OpenClearedDoors()
    {
        foreach (BaseDoor door in MyDoors)
        {
            if (door != null && door.DoorMovedOnce)
            {
                door.OpenDoor();
            }
        }
    }

    public void OpenDoors()
    {
        foreach (BaseDoor door in MyDoors)
        {
            if (door != null)
            {
                if (!door.DoorOpen)
                {
                    door.OpenDoor();
                    //Debug.Log("Opening Doors");
                }
            }
        }
    }

    public void CloseDoors()
    {
        StartCoroutine(cDoors());
    }
}
*/