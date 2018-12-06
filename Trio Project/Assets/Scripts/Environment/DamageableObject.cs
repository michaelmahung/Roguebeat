using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : DamageableEnvironmentItemParent
{ 
    public override void Start()
    {
        base.Start();
    }

    public override void Kill()
    {
        GameManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Objects);
        base.Kill();
    }
}
