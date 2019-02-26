
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerProjectile : BaseProjectile
{
    protected override void Awake()
    {
        base.Awake();
        RaycastHitLength = 0.50f;
    }

    protected override void DealDamage(IDamageable<float> thingToDamage)
    {
        thingToDamage.Damage(ProjectileDamage);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent<IDamageable<float>>();

        if (thingHit != null && hitTag != "Player" && hitTag != "Untagged")
        {
            DealDamage(thingHit);
        }

        else if (hitTag == "Wall" || hitTag == "Shield")
        {
            Deactivate();
        }
    }

    protected override void FireRay()
    {
        
    }

    /*public override void OnTriggerEnter(Collider other)
    {

        hitTag = other.gameObject.tag;
        thingHit = other.gameObject.GetComponent<IDamageable<float>>();

        if (thingHit != null && hitTag != "Player")
        {
            thingHit.Damage(projectileDamage);
            if (hitTag == "Wall")
            {
                Deactivate();
            }
            //Deactivate();
        }

        else if (hitTag == "Wall")
        {
            Deactivate();
        }
    }*/
}

