using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnvironmentDoor : BaseDoor
{
    public new void Start()
    {
        base.Start();
        OpenCondition = openCondition.Objects;
    }
}
