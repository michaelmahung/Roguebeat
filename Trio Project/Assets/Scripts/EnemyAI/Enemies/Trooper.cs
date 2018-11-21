using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trooper : EnemyEngagement{

public Transform Player;


	// Use this for initialization
	new void Start () {

	base.Start2();
		Player = HeroPlayer;
		MoveSpeed = 10.0f;
		print("Order");


	EnemyHealth = 10.0f;
		
	}
	
	// Update is called once per frame
	void Update () {

	transform.LookAt (Player);
	SeePlayer();
	}

}
