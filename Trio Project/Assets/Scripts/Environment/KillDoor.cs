using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kill variant of the base door.

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
        //Wait for the GameManager to find all the active spawners then make a list of the spawners in my room.

        yield return new WaitForSeconds(0.1f);
        foreach (SpawnEnemies spawn in GameManager.Instance.ActiveSpawners)
        {
            if (spawn.CurrentRoom == CurrentRoom)
            {
                spawners.Add(spawn);
            }
        }
        yield break; //Stop the coroutine once it's done.
    }

    //Look into the BaseDoor class to see why this is being "overridden".
    //by assing base.OpenDoor(); I am saying: do what the parent class normally does, but also StopSpawners();

    public override void OpenDoor()
    {
        base.OpenDoor();
        StopSpawners();
    }

    public void StopSpawners()
    {
        //Disable all the spawners in my room and remove the spawners from the GameManager instance.

        foreach (SpawnEnemies enemySpawner in spawners)
        {
            //enemySpawner.gameObject.SetActive(false);
            //GameManager.Instance.RemoveSpawner(enemySpawner);
        }
    }
}
