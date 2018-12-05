using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableItem : DamageableEnvironmentItemParent
{
    private DestroyEnvironmentDoor EnvironmentDoor;

    public override void Start()
    {
        base.Start();
        EnvironmentDoor = GameObject.FindObjectOfType<DestroyEnvironmentDoor>();
    }

    public override void Kill()
    {
        EnvironmentDoor.ItemDestroyed();
        base.Kill();
    }
}
