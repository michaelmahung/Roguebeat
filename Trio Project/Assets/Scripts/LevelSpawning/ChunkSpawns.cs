using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawns : MonoBehaviour {

    [SerializeField] private GameObject[] possibleSpawns; //Do through resources?
    [SerializeField] private GameObject spawnLocation; //Set spawn location (center point)

	void Start () {
        //LevelSpawning.FinishedSpawningRooms += SpawnRandom;

        int rand = Random.Range(0, possibleSpawns.Length);

        Instantiate(possibleSpawns[rand], transform.position, Quaternion.identity);
    }

    void SpawnRandom()
    {
        int rand = Random.Range(0, possibleSpawns.Length + 1);

        Instantiate(possibleSpawns[rand], spawnLocation.transform.position, Quaternion.identity, this.transform);
    }
}
