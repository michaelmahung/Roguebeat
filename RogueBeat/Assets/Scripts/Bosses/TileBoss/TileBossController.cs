﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStates
{
    Phase1,
    Phase2,
    Phase3,
    Default
}

public class TileBossController : BossController, IBossController
{
    public BossStates CurrentState;
    public float TileAttackDelay = 5;
    public float WeaponAttackDelay = 5;
    TileBossHealth bossHealth;
    TileBossWeaponController weapon;
    TileController tileController;
    float weaponAttackTimer;
    float tileDelayTimer;
    bool active;

    void Awake()
    {
        tileDelayTimer = TileAttackDelay / 2;
        tileController = GetComponent<TileController>();
        bossHealth = GetComponent<TileBossHealth>();
        weapon = GetComponent<TileBossWeaponController>();
    }

    public override void PlayerEnteredRoom()
    {
        active = true;

        CurrentState = BossStates.Phase1;

        bossHealth.SetValues();
        weapon.SetValues();
        tileController.SetValues();
    }

    public override void PlayerExitedRoom()
    {
        active = false;
    }

    void Update()
    {
        if(active)
        {
            tileDelayTimer += Time.deltaTime;
            weaponAttackTimer += Time.deltaTime;

            if (tileDelayTimer >= TileAttackDelay)
            {
                tileDelayTimer = 0;
                tileController.ActivateTiles(CurrentState);
            }

            if (weaponAttackTimer >= WeaponAttackDelay)
            {
                weaponAttackTimer = 0;
                weapon.Attack(CurrentState, WeaponAttackDelay);
            }
        }
    }
}
