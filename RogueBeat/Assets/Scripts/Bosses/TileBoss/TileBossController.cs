using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases
{
    Phase1,
    Phase2,
    Phase3,
    Default
}

public class TileBossController : BossController, IBossController
{
    public float TileAttackDelay = 5;
    public float WeaponAttackDelay = 5;
    TileBossHealth bossHealth;
    TileBossWeaponController weapon;
    TileController tileController;
    float weaponAttackTimer;
    float tileDelayTimer;
    bool active;

    void RemoveReferences()
    {
        PlayerHealth.PlayerKilled -= ResetBoss;
        SceneGenerator.LoadingNextLevel -= RemoveReferences;
    }

    void Awake()
    {
        tileDelayTimer = TileAttackDelay / 2;
        tileController = GetComponent<TileController>();
        bossHealth = GetComponent<TileBossHealth>();
        weapon = GetComponent<TileBossWeaponController>();
        PlayerHealth.PlayerKilled += ResetBoss;
        SceneGenerator.LoadingNextLevel += RemoveReferences;
    }

    public void ResetBoss()
    {
        PlayerExitedRoom();
        bossHealth.SetValues();
        weapon.SetValues();
        tileController.SetValues();
    }

    public override void PlayerEnteredRoom()
    {
        weaponAttackTimer = 0;
        tileDelayTimer = 0;

        bossHealth.SetValues();
        weapon.SetValues();
        tileController.SetValues();

        active = true;

        bossHealth.SliderObject.SetActive(true);

        currentPhase = BossPhases.Phase1;
    }

    public override void PlayerExitedRoom()
    {
        active = false;
        bossHealth.SliderObject.SetActive(false);
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
                tileController.ActivateTiles(currentPhase);
            }

            if (weaponAttackTimer >= WeaponAttackDelay)
            {
                weaponAttackTimer = 0;
                weapon.Attack(currentPhase, WeaponAttackDelay);
            }
        }
    }
}
