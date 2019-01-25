using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    public float projectileDamage { get; set; }
    public float projectileSpeed { get; set; }
    public float activeTime { get; set; }

    bool hitEnemy;
    bool hitWall;
    protected string hitTag;
    int shld;
    protected IDamageable<float> thingHit;

    

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
//        ShieldBehavior s = gameObject.GetComponent<ShieldBehavior>();

        //shld = other.gameObject.GetComponent<ShieldBehavior>().health;
        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent <IDamageable<float>>();
        
        if (thingHit != null && hitTag != "Player" && hitTag != "Untagged")
        {
            thingHit.Damage(projectileDamage);
            Deactivate();
        }

        else if (hitTag == "Wall" || hitTag == "Shield")
        {
            //if(hitTag == "Shield")
           // {
                //print("Im hit");
                //other.gameObject.GetComponent<ShieldBehavior>().health--;
            //}
            Deactivate();
            

        }
        

    }
}
