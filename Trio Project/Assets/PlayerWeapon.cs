using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour 
{
    public Weapon weapon;

    private void Start()
    {
        weapon.Print();
    }
}
