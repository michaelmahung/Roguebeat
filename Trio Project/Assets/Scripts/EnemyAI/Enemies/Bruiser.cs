﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruiser : EnemyEngagement {

	// Use this for initialization
	new void Start () {
	base.Start2();
	MoveSpeed = 7.0f;
	EnemyHealth = 15.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
	SeePlayer();
		
	}
}
