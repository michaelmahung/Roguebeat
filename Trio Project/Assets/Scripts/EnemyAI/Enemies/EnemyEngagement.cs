﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagement : EnemyDataModel {

protected float TrooperAttackSpeed = 1.0f; // Trooper initial and adjustable attack speed

protected float BruiserAttackSpeed = 3.0f; // Bruiser initial and adjustable attack speed

protected float BoomerAttackSpeed = 1.0f; // Boomer initial and adjustable attack speed

protected float SetTrooperAttackSpeed; // float to reset Trooper attack speed
protected float SetBruiserAttackSpeed; // float to reset Bruiser attack speed
protected float SetBoomerAttackSpeed; // float to reset Boomer attack speed

	public Transform HeroPlayer;


	// Use this for initialization
	public void Start2 () {
	base.Start();
	Debug.Log("EnemyEngagement");
		HeroPlayer = GameObject.FindGameObjectWithTag("Player").transform; // Locate player via tag to look at and chase
		SetTrooperAttackSpeed = TrooperAttackSpeed; // assigns reset for Trooper as initial time
		SetBruiserAttackSpeed = BruiserAttackSpeed; // assigns reset for Bruiser as initial time
		SetBoomerAttackSpeed = BoomerAttackSpeed; // assigns reset for Boomer as initial time
		print("Timely");
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (Hero);
	SeePlayer();
		//transform.LookAt (Hero); // cause enemies to look at Player
		if (gameObject.name != "Boomer") {
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			//Tells all enemies EXCEPT Boomer type to move forward, which should be facing Player
		}

		TrooperAttackSpeed -= Time.deltaTime; // timer on Trooper attack speed
		BruiserAttackSpeed -= Time.deltaTime; // timer on Bruiser attack speed
		BoomerAttackSpeed -= Time.deltaTime; // timer on Boomer attack speed

		if (TrooperAttackSpeed <= 0 && gameObject.name == "Trooper") {
			Instantiate (EnemyWeapons[0], transform.position, transform.rotation);
			TrooperAttackSpeed = SetTrooperAttackSpeed;
		} else if (BruiserAttackSpeed <= 0 && gameObject.name == "Bruiser") {
			Instantiate (EnemyWeapons[1], transform.position, transform.rotation);
			BruiserAttackSpeed = SetBruiserAttackSpeed;
		} else if (BoomerAttackSpeed <= 0 && gameObject.name == "Boomer") {
			Instantiate (EnemyWeapons[2], transform.position, transform.rotation);
			BoomerAttackSpeed = SetBoomerAttackSpeed;
		}

		if (BoomerHP <= 0) {
		Destroy(gameObject);
		}
		if (EnemyHealth <= 0) {
		Destroy(gameObject);
		}
		if (BruiserHP <= 0) {
		Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "PlayerBaseShot" && gameObject.name == "Boomer") {
			BoomerHP -= 2;
		} 
		/*else if (other.gameObject.tag == "PlayerBaseShot" && gameObject.name == "Trooper") {
			TrooperHP -= 2;

		} 
		*/
		else if (other.gameObject.tag == "PlayerBaseShot" && gameObject.name == "Bruiser") {
			BruiserHP -= 2;
		}
	}

	public void SeePlayer ()
	{
		transform.LookAt (HeroPlayer); // cause enemies to look at Player
	}
}

