using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void RemoveSpawners(RoomSetter room)
    {
        try
        {
            foreach (SpawnEnemies spawner in room.MySpawners)
            {
                spawner.gameObject.SetActive(false);
            }
        } catch
        {
            Debug.Log("Error deleting spawners in room: " + room.RoomName);
        }
    }

    public void AddToDoor(RoomSetter room)
    {
        room.MyDoor.AddToDoor();
    }
}
