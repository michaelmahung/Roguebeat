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
    }

    public override void Update()
    {
        base.Update();

        //Mouse input needs to be updated to allow for various controllers
        if (Input.GetMouseButton(0))
        {
            Fire();
        }

        if (Input.GetMouseButtonUp(0))
        {
            firing = false;
            GameManager.Instance.WeaponSounds.StopAudio();
        }
    }

    public override void Fire()
    {
        if (canFire && !GameManager.Instance.UI.GamePaused)
        {
            canFire = false;
            fireTimer = fireRate;

            if (!firing)
            {
                GameManager.Instance.WeaponSounds.PlayClip(fireSoundInfo);
                firing = true;
            }
            ShootWeapon();

        } 
    }

    public override void OnEnable()
    {
        firing = false;
        base.OnEnable();
    }

    public override void OnDisable()
    {
        if (GameManager.Instance.WeaponSounds != null)
        {
            GameManager.Instance.WeaponSounds.StopAudio();
        }

        base.OnDisable();
    }


}

