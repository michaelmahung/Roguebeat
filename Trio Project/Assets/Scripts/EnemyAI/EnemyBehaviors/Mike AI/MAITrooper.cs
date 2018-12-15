using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAITrooper : MAIData {

    public override void Start()
    {
        base.Start();
        KillPoints = 10;
    }

}
