using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon 
{
    private static Shotgun _instance;
    public static Shotgun Instance { get { return _instance; } }

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
