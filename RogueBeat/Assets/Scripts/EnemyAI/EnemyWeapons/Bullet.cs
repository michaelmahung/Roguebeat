using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyProjectile {

public float BulletLife = 6.0f; //Changed to current and projectile life
public float BulletSpeed = 50.0f;

    // Use this for initialization

    protected override void Awake()
    {
        ProjectileLife = 6.0f;
        base.Awake();
    }

    void Start () {
        //ProjectileLife = 6.0f;
        Damage = 1 * GameManager.Instance.Difficulty;
	}

    // Update is called once per frame
    void Update ()
	{
		currentLife -= Time.deltaTime;
		if (currentLife <= 0) {
            DisableObject();
		}
		if (currentLife > 0) {
			transform.position += transform.forward * BulletSpeed * Time.deltaTime;
		}
	}
}