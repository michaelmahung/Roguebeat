using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon 
{
    new void Start()
    {
        base.Start();
        GetWeaponActive();
    }
}
