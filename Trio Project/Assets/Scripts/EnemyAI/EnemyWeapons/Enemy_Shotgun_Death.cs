using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shotgun_Death : EnemyProjectile {

	public float ShotgunLife = 6.1f;

    protected override void Awake()
    {
        ProjectileLife = 6.1f;
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        Damage = 15;
	}
	
	// Update is called once per frame
	void Update ()
	{

		currentLife -= Time.deltaTime;
		if (currentLife <= 0) {
            DisableObject();
		}
	}
}

