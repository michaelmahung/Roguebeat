using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRoomScript : MonoBehaviour, ITrackRooms
{

    public string MyRoomName { get; set; }
    public RoomSetter MyRoom { get; set; }

    [SerializeField]
    SpawnEnemies[] AllSpawners;
   
    public int DesiredDoors;
    private int ActiveDoors; // in that holds all the door values

    bool spawnersActivated;

    // Use this for initialization
    void Start()
    {
        LevelSpawning.FinishedSpawningRooms += SetComponents;

        AllSpawners = GetComponentsInChildren<SpawnEnemies>();

		foreach(SpawnEnemies spawners in AllSpawners)
		{
			spawners.gameObject.SetActive(false);
		} 
        SelectSpawnDoors();

    }

    void SetComponents()
    {
        RoomSetter.UpdatePlayerRoom += ToggleSpawning;
        MyRoom = GetComponentInParent<RoomSetter>();
        
        foreach(SpawnEnemies spawner in AllSpawners)
        {
            spawner.MyRoom = MyRoom;
        }

        MyRoom.MySpawners = AllSpawners;
    }

    void ToggleSpawning()
    {
        if (!spawnersActivated)
        {
            spawnersActivated = true;
            SelectSpawnDoors();
        }

        /*if (GameManager.Instance.PlayerRoom == MyRoom)
        {
            foreach(SpawnEnemies spawner in AllSpawners)
            {
                if (spawner.gameObject.activeInHierarchy)
                spawner.IsSpawning = true;
            }
        } else
        {
            foreach(SpawnEnemies spawner in AllSpawners)
            {
                if (spawner.gameObject.activeInHierarchy)
                spawner.IsSpawning = false;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            SelectSpawnDoors();
        }
        */

    }

    void SelectSpawnDoors()
    {
        while (ActiveDoors < DesiredDoors)
        {
            int random = Random.Range(0, AllSpawners.Length);

            //Debug.Log(random);

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
}
