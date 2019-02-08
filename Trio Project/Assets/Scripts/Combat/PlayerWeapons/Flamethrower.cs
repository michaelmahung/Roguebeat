using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : BaseWeapon
{
    bool firing;
    private static Flamethrower _instance;
    public static Flamethrower Instance { get { return _instance; } }

    new void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        base.Awake();
    }

    new void Start()
    {
        base.Start();
        SetWeaponActive(true);
        GetWeaponActive();
    }

    public override void Update()
    {
        //Mouse input needs to be updated to allow for various controllers
        if (Input.GetMouseButton(0))
        {
            Fire();
        }

        if (Input.GetMouseButtonUp(0))
        {
            firing = false;
            AudioManager.Instance.StopSound(fireSound.name);
        }
    }

    public override void Fire()
    {
        if (canFire && !GameManager.Instance.UI.GamePaused)
        {
            canFire = false;
            StartCoroutine(WeaponCooldown());
            if (!firing)
            {
                AudioManager.Instance.PlaySound(fireSound.name);
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

    public override void OnDisable()
    {
        AudioManager.Instance.StopSound(fireSound.name);
        base.OnDisable();
    }


}

