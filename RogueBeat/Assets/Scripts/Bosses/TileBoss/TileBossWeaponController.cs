﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBossWeaponController : MonoBehaviour
{
    [SerializeField] TileBossWeapon[] Phase1Weapons;
    [SerializeField] TileBossWeapon[] Phase2Weapons;
    [SerializeField] TileBossWeapon[] Phase3Weapons;
    bool active;
    TileBossWeapon currentWeapon;
    TileBossWeapon previousWeapon;

    public void SetValues()
    {
        foreach(TileBossWeapon wep in Phase3Weapons)
        {
            if (wep != null)
                wep.ResetWeapon();
        }

        currentWeapon = null;
        previousWeapon = null;
    }

    public void Attack(BossPhases state, float attackSpeed)
    {
        switch (state)
        {
            case BossPhases.Phase1:
                SelectNewWeapon(Phase1Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossPhases.Phase2:
                SelectNewWeapon(Phase2Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossPhases.Phase3:
                SelectNewWeapon(Phase3Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossPhases.Default:
                break;
            default:
                break;
        }
    }

    void SelectNewWeapon(TileBossWeapon[] phase)
    {
        int randomInt = Random.Range(0, phase.Length);

        previousWeapon = currentWeapon;
        currentWeapon = phase[randomInt];
    }

    void FireWeapon(float _speed)
    {
        currentWeapon.Fire(_speed);
    }
}
