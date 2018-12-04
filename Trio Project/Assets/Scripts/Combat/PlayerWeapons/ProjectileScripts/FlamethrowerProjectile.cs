using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerProjectile : BaseProjectile
{

    public override void OnTriggerEnter(Collider other)
    {

        string hitTag = other.gameObject.tag;
        IDamageable<float> thingHit = other.gameObject.GetComponent<IDamageable<float>>();

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
    }
}

