///dark yet darker
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public enum KillType { Object, Enemy, MiniBoss };
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
                case KillType.MiniBoss:
                    foreach (BaseDoor door in room.MyDoors)
                    {
                        door.MiniBossKilled();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
