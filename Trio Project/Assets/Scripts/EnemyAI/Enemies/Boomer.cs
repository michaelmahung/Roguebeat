using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : EnemyEngagement {

	// Use this for initialization
	new void Start () {
	base.Start2();
	MoveSpeed = 3.0f;
	EnemyHealth = 25.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
	SeePlayer();
		
	}

	private void FixedUpdate(){
		ChasePlayer();

	}
}
