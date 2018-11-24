using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

public float SpawnTimer = 3.0f;
public bool IsSpawning;
public GameObject[] EnemyTypes;


	// Use this for initialization
	void Start () {
	IsSpawning = true;
		EnemyTypes = Resources.LoadAll<GameObject> ("Prefabs/Enemies");
		if (IsSpawning == true) {
		StartCoroutine(BeginSpawning());
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}


	IEnumerator BeginSpawning ()
	{
	yield return new WaitForSeconds(SpawnTimer);
	Instantiate (EnemyTypes[2], transform.position, transform.rotation);
	StartCoroutine(BeginSpawning());

	}
	}
