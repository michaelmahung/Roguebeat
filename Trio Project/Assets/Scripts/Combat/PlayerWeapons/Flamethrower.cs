using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : BaseWeapon
{
    bool firing;

    new void Start()
    {
        base.Start();
        audioSource.volume = 0.40f;
        audioSource.loop = true;
    }

    public override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }

        if (Input.GetMouseButtonUp(0))
        {
            firing = false;
            audioSource.Stop();
        }
    }

    public override void Fire()
    {
        if (canFire && !GameManager.Instance.gamePaused)
        {
            canFire = false;
            StartCoroutine(WeaponCooldown());
            audioSource.clip = fireSound;
            if (!firing)
            {
                audioSource.Play();
                firing = true;
            }
            ShootWeapon();
        } 
    }

    public override void ShootWeapon()
    {
        for (int i = 0; i < fireLocations.Count; i++)
        {
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation, weaponDamage, projectileSpeed, projectileLife);
        }
    }

    public override void OnEnable()
    {
        firing = false;
        base.OnEnable();
    }


}

