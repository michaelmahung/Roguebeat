using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoors : MonoBehaviour{

    private int enemiesKilled;
    public int enemiesRequired;
    public SpawnEnemies[] spawners;
    bool moved;

    private void Start()
    {
        spawners = GameObject.FindObjectsOfType<SpawnEnemies>();
    }

    void Update ()
	{
		if (enemiesKilled == enemiesRequired && !moved) {
            moved = true;
			transform.Translate (10, 0, 0);
            foreach (SpawnEnemies enemySpawner in spawners)
            {
                enemySpawner.gameObject.SetActive(false);
            }
			print ("moved");
		}
	}

	public void AddKills ()
	{
	    enemiesKilled++;
	    Debug.LogFormat("{0} enemies left", (enemiesRequired - enemiesKilled));
	}
}
