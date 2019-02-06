
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

