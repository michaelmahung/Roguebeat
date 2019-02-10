using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    //All of these variables will be set by the weapon firing the projectile, no need to assign them.
    public float projectileDamage { get; set; }
    public float projectileSpeed { get; set; }
    public float activeTime { get; set; }

    protected float RaycastHitLength { get; set; } //How far ahead of the projectile will it check for objects.
    protected float RayHitDelay { get; set; } //How long to wait after hitting something with a raycast to react to it

    Collider thisCollider;
    bool hitEnemy;
    bool hitWall;
    protected string hitTag;
    int shld;
    protected IDamageable<float> thingHit;
    bool canDamage;

    virtual protected void Awake()
    {
        thisCollider = GetComponent<Collider>();
        RaycastHitLength = 0.25f;
        RayHitDelay = 0.75f;
    }

    public void OnObjectSpawn()
    {
        canDamage = true;
        thisCollider.enabled = true;
        Invoke("Deactivate", activeTime);
    }

    public void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            FireRay();
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        }

        return;
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
            canDamage = false;
            gameObject.SetActive(false);
            hitTag = null;
            thingHit = null;
        }
    }

    //Due to bullets phasing through walls when traveling at high enough speeds, I decided to add a function that will cause each projectile
    //To fire a Raycast forwards while moving - The distance that the raycast will be fired can be set using RaycastHitLength.
    virtual protected void FireRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastHitLength))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            hitTag = hit.transform.tag;
            thingHit = hit.transform.gameObject.GetComponent<IDamageable<float>>();
        }

        if (thingHit != null && hitTag != "Player" && hitTag != "Untagged" && hitTag != "PlayerBaseShot")
        {
            StartCoroutine(LateDamage(thingHit, RayHitDelay));
            //thingHit.Damage(projectileDamage);
        }

        else if (hitTag == "Wall" || hitTag == "Shield" || hitTag == "eProjectile")
        {
            //Debug.Log(hitTag);
            Invoke("Deactivate", RayHitDelay);
        }
    }

    virtual protected void DealDamage(IDamageable<float> thingToDamage)
    {
        //Debug.Log(thingToDamage);
        Deactivate();
        thingToDamage.Damage(projectileDamage);
    }

    virtual protected IEnumerator LateDamage(IDamageable<float> thingToDamage, float delay)
    {
        yield return new WaitForSeconds(delay);
        DealDamage(thingToDamage);
    }

    //Collider-based hit detection, may still be used to very fast objects as insurance.

    virtual public void OnTriggerEnter (Collider other)
	{
        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent <IDamageable<float>>();
        
        if (thingHit != null && hitTag != "Player" && hitTag != "Untagged")
        {
            DealDamage(thingHit);
        }

        else if (hitTag == "Wall" || hitTag == "Shield" || hitTag == "eProjectile")
        {
            Deactivate();
        }
    }
}
