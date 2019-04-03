using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireStates
{
    Idle,
    Firing,
    Reloading
}

//Similar to the tiles, each weapon will handle its own states
//I need to create an update loop that will check the attack speed fed in, and fire + reload within that time frame.
//Doing so will allow me to change the main controller fire speed, and each weapon will follow suit

public class TileBossWeapon : MonoBehaviour
{
    [SerializeField] Transform[] fireLocations;
    [SerializeField] TileBossWeaponController controller;
    FireStates currentState;

    float fireSpeed;

    float fireTimer;

    public void ResetWeapon()
    {
        currentState = FireStates.Idle;
        fireSpeed = 0;
        fireTimer = 0;
    }

    public virtual void Fire(float speed)
    {
        fireSpeed = speed;
        currentState = FireStates.Firing;
    }

    protected virtual void FireWeapon(Transform location)
    {
        GameObject go = GenericPooler.Instance.GrabPrefab(PooledObject.EnemyFire1);
        location.LookAt(GameManager.Instance.PlayerObject.transform.position);
        go.transform.position = location.transform.position;
        go.transform.rotation = location.transform.rotation;
        go.SetActive(true);
        Reload();
    }

    protected virtual void Reload()
    {
        currentState = FireStates.Reloading;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case FireStates.Idle:
                break;
            case FireStates.Firing:
                BeginFiring();
                break;
            case FireStates.Reloading:
                currentState = FireStates.Idle;
                break;
            default:
                break;
        }
    }

    void BeginFiring()
    {
        fireTimer += Time.deltaTime / fireSpeed;

        if (fireTimer < 1)
            return;

        foreach (Transform loc in fireLocations)
        {
            FireWeapon(loc);
        }

        fireTimer = 0;
    }
}
