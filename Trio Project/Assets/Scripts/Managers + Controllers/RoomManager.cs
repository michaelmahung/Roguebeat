﻿///dark yet darker
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public enum RoomType { Object, Enemy, MiniBoss, Timed };
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
        if (room.IsCleared) //Make sure room is ready to be cleared before doing so.
        {
            try
            {
                //Debug.Log("Removing spawners in " + room.name);

                foreach (SpawnEnemies spawner in room.MySpawners)
                {
                    spawner.StopAllCoroutines();
                    spawner.gameObject.SetActive(false);
                }
            }
            catch
            {
                Debug.LogError("Error deleting spawners in room: " + room.RoomName);
            }
        }
    }

    public void AddToDoor(RoomSetter room, RoomType type)
    {
        if (room.MyDoors != null)
        {
            switch (type)
            {
                case RoomType.Object:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.ObjectDestroyed();
                    }
                    break;
                case RoomType.Enemy:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.EnemyKilled();
                    }
                    break;
                case RoomType.MiniBoss:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.MiniBossKilled();
                    }
                    break;
                case RoomType.Timed:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.OpenDoor();
                    }
                    break;
                default:
                    break;
            }
        }

        else
        {
            Debug.Log("no doors");
        }
    }
}
