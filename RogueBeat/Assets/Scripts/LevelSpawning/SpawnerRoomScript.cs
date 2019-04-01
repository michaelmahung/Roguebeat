using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRoomScript : MonoBehaviour, ITrackRooms, IRoomBehaviour
{
    public int EnemyCount = 0;
    public int EnemyCap = 6;

    
    public RoomSetter MyRoom { get; set; }
    public bool RoomActive { get; set; } //Adding to allow for new interface

    [SerializeField]
    SpawnEnemies[] AllSpawners;
   
    public int DesiredDoors;
    private int ActiveDoors; // in that holds all the door values

    bool spawnersActivated;

    void Start()
    {
        //LevelSpawning.FinishedSpawningRooms += SetComponents;

        AllSpawners = GetComponentsInChildren<SpawnEnemies>();

		foreach(SpawnEnemies spawners in AllSpawners)
		{
			spawners.gameObject.SetActive(false);
		} 
        SetComponents();

        SelectSpawnDoors();
    }

    void SetComponents()
    {
        MyRoom = GetComponentInParent<RoomSetter>();
        
        foreach(SpawnEnemies spawner in AllSpawners)
        {
            spawner.MyRoom = MyRoom;
        }

        MyRoom.MySpawners = AllSpawners;
    }

    void SelectSpawnDoors()
    {
        while (ActiveDoors < DesiredDoors)
        {
            int random = Random.Range(0, AllSpawners.Length);

            if (!AllSpawners[random].gameObject.activeInHierarchy)
            {
                AllSpawners[random].gameObject.SetActive(true);
                ActiveDoors++;
            }
            else
            {
                SelectSpawnDoors();
            }
        }
		return;
    }

    public bool EnemiesCapped()
    {
        if (EnemyCount >= EnemyCap)
        {
            return true;
        }

        return false;
    }

    public void AddEnemy()
    {
        EnemyCount++;
    }

    public void RemoveEnemy()
    {
        EnemyCount--;
    }

    //Adding logic below for new interface

    public void StartBehaviour()
    {
        RoomActive = true;

        foreach(SpawnEnemies spawner in AllSpawners)
        {
            if (spawner.gameObject.activeInHierarchy && spawner != null)
            {
                spawner.StartSpawns();
            }
        }
    }

    public void StopBehaviour()
    {
        RoomActive = false;

        foreach (SpawnEnemies spawner in AllSpawners)
        {
            if (spawner != null)
            {
                spawner.StopSpawns();
            }
        }
    }
}
