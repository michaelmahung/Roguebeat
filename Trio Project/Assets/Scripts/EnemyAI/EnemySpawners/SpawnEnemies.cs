﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour, ITrackRooms {

public float SpawnTimer = 3.0f;
public bool IsSpawning;
public bool SpawnerTransition;
public float SpawnMovementSpeed;
public bool SpawnMover;
public Transform StartPosition;
public Transform EndPosition;
public GameObject[] EnemyTypes;
public string CurrentRoom { get; set; }
private int RandomChance;
public int EnemyCount;
public int EnemyMaxCount;
private int CounterCheck;
public List<GameObject> EnemyNumbers = new List<GameObject>();


	// Use this for initialization
	void Start ()
	{
        //Subscribe to these events, spawners want to know when the player enters their room and when the player dies.
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
        PlayerHealth.PlayerKilled += StopSpawns;
	    SpawnMover = false;
		EnemyTypes = Resources.LoadAll<GameObject> ("Prefabs/Enemies");
	}
	
	// Update is called once per frame
	void Update ()
	{
	float step = SpawnMovementSpeed * Time.deltaTime;
		if (SpawnMover == false) {
			transform.position = Vector3.MoveTowards(transform.position, EndPosition.position, step);

            //Since floats are never really equal to each other I just said, if the distance between the two floats is small.
            //Was running into issues where the spawner would stop on one end or the other.
			if (Vector3.Distance(transform.position, EndPosition.position) < 0.01f) {

				SpawnMover = true;
			}
		}

		if (SpawnMover == true) {
			transform.position = Vector3.MoveTowards(transform.position, StartPosition.position, step);
			if (Vector3.Distance(transform.position, StartPosition.position) < 0.01f) {
				SpawnMover = false;
			}
		}
	}

	IEnumerator BeginSpawning ()
	{
		yield return new WaitForSeconds (SpawnTimer);
		RandomChance = Random.Range (1, 100);
		if (EnemyCount <= EnemyMaxCount) {
			if (RandomChance <= 40) {
			CounterCheck++;
				for (EnemyCount = EnemyCount; EnemyCount < CounterCheck && EnemyCount <= EnemyMaxCount; EnemyCount++) {
					EnemyNumbers.Add (Instantiate (EnemyTypes [0], transform.position, transform.rotation) as GameObject);
					//Instantiate (EnemyTypes [2], transform.position, transform.rotation);
				}
			}

			if (RandomChance > 40 && RandomChance < 80) {
				CounterCheck++;
				for (EnemyCount = EnemyCount; EnemyCount < CounterCheck && EnemyCount <= EnemyMaxCount; EnemyCount++) {
					EnemyNumbers.Add (Instantiate (EnemyTypes [1], transform.position, transform.rotation) as GameObject);
					//Instantiate (EnemyTypes [1], transform.position, transform.rotation);
				}
			}

			if (RandomChance >= 80) {
				CounterCheck++;
				for (EnemyCount = EnemyCount; EnemyCount < CounterCheck && EnemyCount <= EnemyMaxCount; EnemyCount++) {
					EnemyNumbers.Add (Instantiate (EnemyTypes [2], transform.position, transform.rotation) as GameObject);
				}

				//Instantiate (EnemyTypes [0], transform.position, transform.rotation);
			}


			StartCoroutine (BeginSpawning ());
		}
	}

    void CheckPlayerRoom()
    {
        if (gameObject.activeInHierarchy == true)
        {
            if (GameManager.Instance.PlayerRoom == CurrentRoom)
            {
                StartSpawns();
            }
            else
            {
                StopSpawns();
            }
        } 
    }

    void StartSpawns()
    {

        IsSpawning = true;
        StartCoroutine(BeginSpawning());
    }

    void StopSpawns()
    {
        IsSpawning = false;
        StopAllCoroutines();
    }

}
