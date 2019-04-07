using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBurstRifle : TileBossWeapon
{
    [SerializeField] float burstBuffer = 0.25f;
    [SerializeField] float shotDelay;
    [SerializeField] float totalShots = 3;

    int burstCount;

    public override void ResetWeapon()
    {
        base.ResetWeapon();
        burstCount = 0;
    }

    public override void Fire(float speed)
    {
        fireSpeed = speed;
        shotDelay = fireSpeed / (totalShots + burstBuffer);

        currentState = FireStates.Firing;
    }

    protected override void Reload()
    {
        if (burstCount < totalShots - 1)
        {
            currentState = FireStates.Firing;
            burstCount++;
            return;
        }

        currentState = FireStates.Reloading;
        burstCount = 0;
    }

    protected override void BeginFiring()
    {
        fireTimer += Time.deltaTime / shotDelay;

        if (fireTimer < shotDelay)
            return;

        foreach (Transform loc in fireLocations)
        {
            FireWeapon(loc);
        }

        fireTimer = 0;
    }
}
