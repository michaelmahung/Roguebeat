﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerMine : DamageableEnvironmentItemParent {

    [SerializeField] private float baseDamage = 20;
    [SerializeField] private int lifeTime = 60;
    IDamageable<float> damageable;

    private float damage
    {
        get { return baseDamage * GameManager.Instance.Difficulty; }
    }

    new void Start()
    {
        base.Start();
        ItemType = myItemType.Metal;
        Armor = 1;
        KillPoints = 10;
        Invoke("SelfDestruct", lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SelfDestruct();
            damageable = other.GetComponent<IDamageable<float>>();
            damageable.Damage(damage);
        }
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

}
