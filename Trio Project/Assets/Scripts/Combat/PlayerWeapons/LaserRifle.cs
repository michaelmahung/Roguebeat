using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRifle : BaseWeapon
{
    private static LaserRifle _instance;
    public static LaserRifle Instance { get { return _instance; } }

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
}
