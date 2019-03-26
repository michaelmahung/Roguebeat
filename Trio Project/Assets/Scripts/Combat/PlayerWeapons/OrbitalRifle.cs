using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalRifle : BaseWeapon
{
    private static OrbitalRifle _instance;
    public static OrbitalRifle Instance { get { return _instance; } }

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

}
