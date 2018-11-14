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
        projectileSpawnLocations[0] = player.transform.position + new Vector3 (0, 0, 0.5f);
        ProjectilePoolManager.Instance.SpawnFromPool(projectileName, projectileSpawnLocations, player.transform.rotation);
    }
}
