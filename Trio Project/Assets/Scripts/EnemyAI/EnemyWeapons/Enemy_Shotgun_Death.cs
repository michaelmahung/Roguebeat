using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shotgun_Death : EnemyProjectile {

	public float ShotgunLife = 6.1f;

	// Use this for initialization
	void Start () {
        Damage = 15;
	}
	
	// Update is called once per frame
	void Update ()
	{

		ShotgunLife -= Time.deltaTime;
		if (ShotgunLife <= 0) {
			Destroy (gameObject);
		}
	}
}

