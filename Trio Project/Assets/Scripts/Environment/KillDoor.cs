using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDoor : BaseDoor
{
    private int enemiesKilled;
    public int enemiesRequired;
    public SpawnEnemies[] spawners;

    public new void Start()
    {
        base.Start();
        spawners = GameObject.FindObjectsOfType<SpawnEnemies>();
    }

    public void StopSpawners()
    {
        foreach (SpawnEnemies enemySpawner in spawners)
        {
            enemySpawner.gameObject.SetActive(false);
        }
    }

	public void AddKills ()
	{
	    enemiesKilled++;

        if (enemiesKilled >= enemiesRequired && !doorMoved)
        {
            StopSpawners();
            OpenDoor();
        }

        Debug.LogFormat("{0} enemies left", (enemiesRequired - enemiesKilled));
    }
}
