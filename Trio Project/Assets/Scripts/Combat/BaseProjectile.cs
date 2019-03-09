﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    //All of these variables will be set by the weapon firing the projectile, no need to assign them.
    public float ProjectileDamage { get; set; }
    public float ProjectileSpeed { get; set; }
    public float ActiveTime { get; set; }

    protected float RaycastHitLength { get; set; } //How far ahead of the projectile will it check for objects.
    protected float RayHitDelay { get; set; } //How long to wait after hitting something with a raycast to react to it
    protected string[] DisableList { get; set; }

    Collider thisCollider;
    protected string hitTag;
    protected IDamageable<float> thingHit;
    private float activerTimer;

    TagManager Tags;

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            //FireRay();
            transform.position += transform.forward * ProjectileSpeed * Time.deltaTime;
        }

        return;
    }

    protected void Update()
    {
        activerTimer += Time.deltaTime;

        if (activerTimer > ActiveTime)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        if (gameObject.activeInHierarchy)
        {
            //Debug.Log("Deactivating");
            if (thisCollider != null)
            {
                thisCollider.enabled = false;
            }
            activerTimer = 0;
            gameObject.SetActive(false);
            hitTag = null;
            thingHit = null;
        }
    }

    virtual protected void Awake()
    {
        Tags = GameManager.Instance.Tags;
        thisCollider = GetComponent<Collider>();
        RaycastHitLength = 0.25f;
        RayHitDelay = 0.75f;
    }

    //HAS to be public since its from the IPooledObject interface.
    public void OnObjectSpawn()
    {
        thisCollider.enabled = true;
        //Invoke("Deactivate", ActiveTime);
    }

    virtual protected void DealDamage(IDamageable<float> thingToDamage)
    {
        //Debug.Log(thingToDamage);
        Deactivate();
        thingToDamage.Damage(ProjectileDamage);
    }

    //Collider-based hit detection, may still be used to very fast objects as insurance.

    virtual protected void OnTriggerEnter (Collider other)
	{
        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent <IDamageable<float>>();
        
        if (thingHit != null && !other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.Untagged))
        {
            DealDamage(thingHit);
        }
        else if (other.CompareTag(Tags.WallTag) || other.CompareTag(Tags.ShieldTag))
        {
            Deactivate();
        }

        /*if (thingHit != null && hitTag != "Player" && hitTag != "Untagged")
        {
            DealDamage(thingHit);
        }

        else if (hitTag == "Wall" || hitTag == "Shield")
        {
            Deactivate();
        }*/
    }
}
