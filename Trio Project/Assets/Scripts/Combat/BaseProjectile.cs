using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    public float projectileDamage;
    public float projectileSpeed;
    public float activeTime;

    bool HitEnemy;
    bool HitWall;

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

        string hitTag = other.gameObject.tag;
        IDamageable <float> thingHit = other.gameObject.GetComponent < IDamageable<float>>();

        if (thingHit != null && hitTag != "Player")
        {
            try
            {
                thingHit.Damage(projectileDamage);
                //Debug.LogFormat("Hit {0} for {1} damage.", other.gameObject.name, projectileDamage);
            }
            catch
            {
                Debug.LogErrorFormat("{0} does not have IDamageable interface attached.", hitTag);
            }
            Deactivate();
        }

        else if (hitTag == "Wall")
        {
            Deactivate();
        }
    }
}
