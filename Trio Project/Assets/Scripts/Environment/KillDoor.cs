using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDoor : BaseDoor
{
    public List<SpawnEnemies> spawners = new List<SpawnEnemies>();

    public new void Start()
    {
        base.Start();
        OpenCondition = openCondition.Kills;
        StartCoroutine(FindSpawners());
    }

    IEnumerator FindSpawners()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (SpawnEnemies spawn in GameManager.Instance.ActiveSpawners)
        {
            if (spawn.CurrentRoom == CurrentRoom)
            {
                spawners.Add(spawn);
            }
        }
        yield break;
    }

    public override void OpenDoor()
    {
        base.OpenDoor();
        StopSpawners();
    }

    public void StopSpawners()
    {
        foreach (SpawnEnemies enemySpawner in spawners)
        {
            enemySpawner.gameObject.SetActive(false);
            GameManager.Instance.RemoveSpawner(enemySpawner);
        }
    }
}
