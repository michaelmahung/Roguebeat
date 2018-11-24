using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRifle : BaseWeapon
{
    new void Start()
    {
        base.Start();
        audioSource.volume = 0.25f;
    }
}
