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
    }

    //Look into the BaseDoor class to see why this is being "overridden".
    //by assing base.OpenDoor(); I am saying: do what the parent class normally does

    public override void OpenDoor()
    {
        base.OpenDoor();
    }
}
