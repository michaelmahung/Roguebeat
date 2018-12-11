using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    [HideInInspector]
    public float projectileDamage;
    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public float activeTime;

    bool HitEnemy;
    bool HitWall;
    string hitTag;
    IDamageable<float> thingHit;

    public void OnObjectSpawn()
    {
        Invoke("Deactivate", activeTime);
    }

    public void FixedUpdate()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    } 

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    virtual public void OnTriggerEnter (Collider other)
	{

        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent <IDamageable<float>>();

        if (thingHit != null && hitTag != "Player")
        {
            thingHit.Damage(projectileDamage);
            Deactivate();
        }

        else if (hitTag == "Wall")
        {
            Deactivate();
        }
    }
}
