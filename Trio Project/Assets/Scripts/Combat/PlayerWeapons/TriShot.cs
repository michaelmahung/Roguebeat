using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : BaseWeapon 
{
    private static TriShot _instance;
    public static TriShot Instance { get { return _instance; } }

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

    //Nothing to see here
    new void Start()
    {
        base.Start();
    }
}
