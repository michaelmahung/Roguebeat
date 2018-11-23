using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : BaseWeapon
{
    //Here we are overriding the empty update function with one that simply fires the weapon
    public override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    public override void ShootWeapon()
    {
        //We also need to create a function for the abstract ShootWeapon function, so we do that here.
        //The logic says that for every location assigned to fire projectiles from, grab a projectile from the pool and fire it.
    }
}
