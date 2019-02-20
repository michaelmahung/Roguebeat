///dark yet darker
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public enum KillType { Object, Enemy };
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
                foreach (SpawnEnemies spawner in room.MySpawners)
                {
                    spawner.gameObject.SetActive(false);
                }
            }
            catch
            {
                Debug.Log("Error deleting spawners in room: " + room.RoomName);
            }
        }
    }

    public void AddToDoor(RoomSetter room, KillType type)
    {
        if (room.MyDoors != null)
        {
            switch (type)
            {
                case KillType.Object:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.ObjectDestroyed();
                    }
                    break;
                case KillType.Enemy:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.EnemyKilled();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
