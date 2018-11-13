using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Attack : EnemyDetection {

public float AttackSpeed = 20.0f;
public float SetAttackSpeed;


	// Use this for initialization
	void Start () {
		Hero = GameObject.FindGameObjectWithTag("Player").transform;
		SetAttackSpeed = AttackSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.LookAt (Hero);
		transform.position += transform.forward * MoveSpeed * Time.deltaTime;

		AttackSpeed -= Time.deltaTime;
		if (AttackSpeed <= 0) {
			Instantiate (EnemyFire1, transform.position, transform.rotation);
			Reset();

		}


	}

	void Reset ()
	{

		AttackSpeed = SetAttackSpeed;
	}

	}
