using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserRifle : TileBossWeapon
{
    [SerializeField] float laserBuffer = 0.10f;
    [SerializeField] float laserChargePercentage = 0.3f;
    [SerializeField] float laserTimer;

    public override void Fire(float speed)
    {
        fireSpeed = speed - laserBuffer;

        currentState = FireStates.Firing;
    }

    protected override void BeginFiring()
    {
        fireTimer += Time.deltaTime / fireSpeed;

        if (fireTimer < laserChargePercentage)
        {
            ChargeLaser();
            return;
        }

        if (fireTimer < 1)
        {
            foreach (Transform loc in fireLocations)
            {
                FireWeapon(loc);
            }

            return;
        }

        Reload();
    }

    protected override void Reload()
    {
        currentState = FireStates.Reloading;
        fireTimer = 0;
    }

    protected override void FireWeapon(Transform location)
    {
        GameObject go = GenericPooler.Instance.GrabPrefab(myProjectile);
        location.LookAt(GameManager.Instance.PlayerObject.transform.position);
        go.transform.position = location.transform.position;
        go.transform.rotation = location.transform.rotation;
        go.SetActive(true);
    }

    void ChargeLaser()
    {
        Debug.Log("CHARGING");
    }
}
