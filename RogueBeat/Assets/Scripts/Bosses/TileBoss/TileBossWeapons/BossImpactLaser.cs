using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BossImpactLaser : TileBossWeapon
{
    [SerializeField] int shotsPerAttack;
    [SerializeField] float shotDelayTime;
    IDamageable<float> playerDamage;
    Vector3 playerTempLocation;
    float delayTimer;
    float shotIncrementTimer;
    int shotsTaken;
    LineRenderer lr;
    RaycastHit hit;
    bool charging;

    private new void Awake()
    {
        base.Awake();
        playerDamage = GameManager.Instance.PlayerHealthRef.GetComponent<IDamageable<float>>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, fireLocations[0].transform.position);
        lr.enabled = false;
    }

    public override void ResetWeapon()
    {
        base.ResetWeapon();
        lr.enabled = false;
        delayTimer = 0;
        shotIncrementTimer = 0;
        shotsTaken = 0;
        charging = false;
    }

    public override void Fire(float speed)
    {
        shotIncrementTimer = speed / shotsPerAttack;
        lr.enabled = true;
        base.Fire(speed);
    }

    protected override void Update()
    {
        switch (currentState)
        {
            case FireStates.Idle:
                charging = false;
                break;
            case FireStates.Charging:
                charging = true;
                BeginFiring();
                ChargeLaser();
                break;
            case FireStates.Firing:
                charging = false;
                BeginFiring();
                break;
            case FireStates.Reloading:
                currentState = FireStates.Idle;
                lr.enabled = false;
                break;
            default:
                break;
        }
    }

    protected override void BeginFiring()
    {
        lr.SetPosition(1, playerTempLocation);
        lr.enabled = true;

        if (!charging)
        {
            playerTempLocation = GameManager.Instance.PlayerObject.transform.position;
            lr.SetPosition(1, playerTempLocation);
        }

        if (shotsTaken < shotsPerAttack)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer < shotIncrementTimer - shotDelayTime)
                return;

            currentState = FireStates.Charging;

            if (fireTimer > shotIncrementTimer)
                fireTimer = 0;

            return;
        }

        Reload();
    }

    protected override void FireWeapon(Transform location)
    {
        lr.SetPosition(1, playerTempLocation);
        location.LookAt(playerTempLocation);
        currentState = FireStates.Firing;

        if (Physics.Raycast(location.transform.position, location.transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Player"))

                lr.SetPosition(1, hit.point);

                var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                GameObject go = GenericPooler.Instance.GrabPrefab(PooledObject.Explosion);
                go.transform.position = hit.point;
                go.transform.rotation = hitRotation;
                go.SetActive(true);
                GameManager.Instance.CameraShaker.Shake();
            }
        }

        audioSource.Play();
        shotsTaken++;
    }

    protected override void Reload()
    {
        currentState = FireStates.Reloading;
        shotsTaken = 0;
        delayTimer = 0;
        fireTimer = 0;
        lr.enabled = false;
    }

    void ChargeLaser()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer < shotDelayTime)
            return;

        delayTimer = 0;

        foreach (Transform loc in fireLocations)
        {
            FireWeapon(loc);
        }
    }
}
