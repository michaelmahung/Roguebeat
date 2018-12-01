using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerProjectile : BaseProjectile
{

    public override void OnTriggerEnter(Collider other)
    {
        string hitTag = other.gameObject.tag;
        IDamageable<float> thingHit = other.gameObject.GetComponent<IDamageable<float>>();

        if (thingHit != null && hitTag == "Wall")
        {
            thingHit.Damage(projectileDamage);
            Deactivate();
        } 

        if (thingHit != null && hitTag != "Player")
        {
            if (hitTag == "Wall")
            {
                thingHit.Damage(projectileDamage);
                Deactivate();
            }
            try
            {
                thingHit.Damage(projectileDamage);
            }
            catch
            {
                Debug.LogErrorFormat("{0} does not have IDamageable interface attached.", hitTag);
            }
        }

        else if (hitTag == "Wall")
        {
            Deactivate();
        }
    }
}

