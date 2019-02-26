﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour, ITrackRooms {

public float SpawnTimer;
public bool IsSpawning;
public bool SpawnerTransition;
public float SpawnMovementSpeed;
public bool SpawnMover;

public Transform StartPosition;
public Transform EndPosition;
public GameObject[] EnemyTypes;
public GameObject SpawnPoint;
public string MyRoomName { get; set; }
public RoomSetter MyRoom { get; set; }
private int RandomChance;



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
	/*float step = SpawnMovementSpeed * Time.deltaTime;
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
        */
	}

	IEnumerator BeginSpawning ()
	{
        //print(Time.time);
	yield return new WaitForSeconds (SpawnTimer);
    //print(Time.time);
        //print ("I waited");
        

        if (MyRoom.EnemiesCapped() == false)
        {
            RandomChance = Random.Range(1, 100);
            //print (RandomChance);
            if (RandomChance <= 40)
            {
                print ("Trooper");
                if(gameObject.name != "SpawnDoor"){

                    //Caching the object spawned so we can tell it what room to assign itself to - Mike
                    //This should fix the issue where enemies are spawning across rooms and not activating properly

                    GameObject go = Instantiate(EnemyTypes[2], transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
                else if(gameObject.name == "SpawnDoor")
                {
                   GameObject go = Instantiate(EnemyTypes[2], SpawnPoint.transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
                
            }
         

            if (RandomChance > 40 && RandomChance < 80)
            {
                 print ("Gunner");
                if(gameObject.name != "SpawnDoor"){
                    GameObject go = Instantiate(EnemyTypes[1], transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
                else if(gameObject.name == "SpawnDoor")
                {
                    GameObject go = Instantiate(EnemyTypes[1], SpawnPoint.transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
            

            }

            if (RandomChance >= 80)
            {
                 print ("Boomer");
                if(gameObject.name != "SpawnDoor"){
                    GameObject go = Instantiate(EnemyTypes[0], transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
                else if(gameObject.name == "SpawnDoor")
                {
                    GameObject go = Instantiate(EnemyTypes[0], SpawnPoint.transform.position, transform.rotation);
                    ITrackRooms room = go.GetComponent<ITrackRooms>();
                    room.MyRoom = MyRoom;
                }
                
            }

            MyRoom.AddEnemy();
        }
        StartCoroutine(BeginSpawning());
    }

    void CheckPlayerRoom()
    {
        if (gameObject.activeInHierarchy == true)
        {
            if (GameManager.Instance.PlayerRoom == MyRoom)
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
