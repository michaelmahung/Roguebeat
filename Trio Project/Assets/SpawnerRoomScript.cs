using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRoomScript : MonoBehaviour
{

    [SerializeField]
    SpawnEnemies[] FruitLoops;
   
    public int DesiredDoors;
    private int ActiveDoors; // in that holds all the door values

    // Use this for initialization
    void Start()
    {
        //LevelSpawning.FinishedSpawningRooms += SelectSpawnDoors;
        FruitLoops = GetComponentsInChildren<SpawnEnemies>();
		foreach(SpawnEnemies spawners in FruitLoops)
		{
			spawners.gameObject.SetActive(false);
		} 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SelectSpawnDoors();
        }

    }

    void SelectSpawnDoors()
    {
        while (ActiveDoors < DesiredDoors)
        {
            int random = Random.Range(0, FruitLoops.Length);

            Debug.Log(random);

            if (!FruitLoops[random].gameObject.activeInHierarchy)
            {
                FruitLoops[random].gameObject.SetActive(true);
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
