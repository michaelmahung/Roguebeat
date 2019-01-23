using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : BaseWeapon
{
    private static PeaShooter _instance;
    public static PeaShooter Instance { get { return _instance; } }

    new void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }else
        {
            _instance = this;
        }

        base.Awake();
    }

    new void Start()
    {
        base.Start();
        GetWeaponActive();
    }
}
