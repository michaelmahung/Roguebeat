using System.Collections;
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

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            FireRay();
            transform.position += transform.forward * ProjectileSpeed * Time.deltaTime;
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
            gameObject.SetActive(false);
            hitTag = null;
            thingHit = null;
        }
    }

    virtual protected void Awake()
    {
        thisCollider = GetComponent<Collider>();
        RaycastHitLength = 0.25f;
        RayHitDelay = 0.75f;
    }

    //HAS to be public since its from the IPooledObject interface.
    public void OnObjectSpawn()
    {
        thisCollider.enabled = true;
        Invoke("Deactivate", ActiveTime);
    }

    //Due to bullets phasing through walls when traveling at high enough speeds, I decided to add a function that will cause each projectile
    //To fire a Raycast forwards while moving - The distance that the raycast will be fired can be set using RaycastHitLength.
    //Im not sure about this performance-wise, but it has helped significantly - Unity will release a new version with better
    //collision detection but I don't think we wan't to upgrade versions in the middle of development.
    virtual protected void FireRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastHitLength))
        {
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
        thingToDamage.Damage(ProjectileDamage);
    }

    virtual protected IEnumerator LateDamage(IDamageable<float> thingToDamage, float delay)
    {
        yield return new WaitForSeconds(delay);
        DealDamage(thingToDamage);
    }

    //Collider-based hit detection, may still be used to very fast objects as insurance.

    virtual protected void OnTriggerEnter (Collider other)
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
