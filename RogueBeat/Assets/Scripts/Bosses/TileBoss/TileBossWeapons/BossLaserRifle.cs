using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BossLaserRifle : TileBossWeapon
{
    [SerializeField] float laserLead = 0.1f;
    [SerializeField] float laserLength = 1000;
    [SerializeField] float laserBuffer = 0.10f;
    [SerializeField] float laserChargePercentage = 0.25f;
    [SerializeField] float laserTrackSpeed = 0.95f;
    [SerializeField] float laserTimer;
    [SerializeField] float laserDamage = 0.10f;
    float trackLoc;
    Vector3 laserStartPosition;
    LineRenderer lr;
    RaycastHit hit;
    IDamageable<float> playerDamage;

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
        laserTimer = 0;
        trackLoc = 0;
        lr.enabled = false;
    }

    public override void Fire(float speed)
    {
        fireSpeed = speed - laserBuffer;
        lr.enabled = true;
        lr.SetPosition(1, fireLocations[0].transform.position + Vector3.forward * 2);
        laserStartPosition = GameManager.Instance.PlayerObject.transform.position;
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
        audioSource.Stop();
        lr.enabled = false;
        currentState = FireStates.Reloading;
        fireTimer = 0;
        trackLoc = 0;
    }

    protected override void FireWeapon(Transform location)
    {
        //TODO - make laser move a set amount between old - new position per frame -- use Vector3.normalized to find angle to move in.
        
        trackLoc += (Time.deltaTime * laserTrackSpeed) / fireSpeed;
  
        location.LookAt(Vector3.Slerp(laserStartPosition, GameManager.Instance.PlayerObject.transform.position, (trackLoc + laserLead)));

        audioSource.Play();

        if (Physics.Raycast(location.transform.position, location.transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

                if (hit.collider.CompareTag("Player"))
                {
                    playerDamage.Damage(laserDamage);
                    return;
                }

                if (hit.collider.CompareTag("Floor"))
                {
                    var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    GameObject go = GenericPooler.Instance.GrabPrefab(PooledObject.LaserDecal);
                    go.transform.position = hit.point;
                    go.transform.rotation = hitRotation;
                    go.SetActive(true);
                    return;
                }
            }
        }
    }

    void ChargeLaser()
    {
        //Debug.Log("CHARGING");
    }
}
