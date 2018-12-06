using System.Collections;
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


	// Use this for initialization
	void Start ()
	{
	    SpawnMover = false;
		IsSpawning = true;
		EnemyTypes = Resources.LoadAll<GameObject> ("Prefabs/Enemies");
		if (IsSpawning == true) {
			StartCoroutine (BeginSpawning ());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	float step = SpawnMovementSpeed * Time.deltaTime;
		if (SpawnMover == false) {
			transform.position = Vector3.MoveTowards(transform.position, EndPosition.position, step);
			if (transform.position == EndPosition.position) {

				SpawnMover = true;
			}
		}

		if (SpawnMover == true) {
			transform.position = Vector3.MoveTowards(transform.position, StartPosition.position, step);
			if (transform.position == StartPosition.position) {
				SpawnMover = false;
			}
		}
	}


	IEnumerator BeginSpawning ()
	{
		yield return new WaitForSeconds (SpawnTimer);
		RandomChance = Random.Range (1, 100);
		if (RandomChance <= 40) {
			Instantiate (EnemyTypes [2], transform.position, transform.rotation);
		}

		if (RandomChance > 40 && RandomChance < 80) {
			Instantiate (EnemyTypes [1], transform.position, transform.rotation);
		}

		if (RandomChance >= 80) {
			Instantiate (EnemyTypes [0], transform.position, transform.rotation);
		}


		StartCoroutine (BeginSpawning ());
	}
	}
