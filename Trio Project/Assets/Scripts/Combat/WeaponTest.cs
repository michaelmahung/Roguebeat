using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : BaseWeapon
{


    public override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    public override void ShootWeapon()
    {
        for (int i = 0; i < fireLocations.Count; i++)
        {
            //player.transform.rotation
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation);
        }
    }
}
