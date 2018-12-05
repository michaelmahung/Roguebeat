using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoors : MonoBehaviour{

    private int enemiesKilled;
    public int enemiesRequired;
    public SpawnEnemies[] spawners;
    bool moved;
    private Vector3 StopDoor;
    private Vector3 StartLocation;
    public GameObject endDoorLocation;
    public float speed;

    private void Start()
    {
    StartLocation = transform.position;
    StopDoor = endDoorLocation.transform.position;
    spawners = GameObject.FindObjectsOfType<SpawnEnemies>();
    }

    void Update ()
	{
	float step = speed * Time.deltaTime;
		if (enemiesKilled == enemiesRequired && !moved) {
			transform.position = Vector3.MoveTowards (transform.position, StopDoor, step );
			if (StartLocation == StopDoor) {
			moved = true;
			}
            foreach (SpawnEnemies enemySpawner in spawners)
            {
                enemySpawner.gameObject.SetActive(false);
            }
		}
	}

	public void AddKills ()
	{
		enemiesKilled++;
		Debug.LogFormat ("{0} enemies left", (enemiesRequired - enemiesKilled));
	}

}
