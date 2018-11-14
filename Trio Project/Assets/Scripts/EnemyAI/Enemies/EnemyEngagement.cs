using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagement : EnemyDetection {

[SerializeField]
private float TrooperAttackSpeed = 1.0f; // Trooper initial and adjustable attack speed

[SerializeField]
private float BruiserAttackSpeed = 3.0f; // Bruiser initial and adjustable attack speed

[SerializeField]
private float BoomerAttackSpeed = 1.0f; // Boomer initial and adjustable attack speed

private float SetTrooperAttackSpeed; // float to reset Trooper attack speed
private float SetBruiserAttackSpeed; // float to reset Bruiser attack speed
private float SetBoomerAttackSpeed; // float to reset Boomer attack speed


	// Use this for initialization
	void Start () {

		Hero = GameObject.FindGameObjectWithTag("Player").transform; // Locate player via tag to look at and chase
		SetTrooperAttackSpeed = TrooperAttackSpeed; // assigns reset for Trooper as initial time
		SetBruiserAttackSpeed = BruiserAttackSpeed; // assigns reset for Bruiser as initial time
		SetBoomerAttackSpeed = BoomerAttackSpeed; // assigns reset for Boomer as initial time
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (Hero); // cause enemies to look at Player
		if (gameObject.name != "Boomer") {
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			//Tells all enemies EXCEPT Boomer type to move forward, which should be facing Player
		}

		TrooperAttackSpeed -= Time.deltaTime; // timer on Trooper attack speed
		BruiserAttackSpeed -= Time.deltaTime; // timer on Bruiser attack speed
		BoomerAttackSpeed -= Time.deltaTime; // timer on Boomer attack speed

		if (TrooperAttackSpeed <= 0 && gameObject.name == "Trooper") {
			Instantiate (EnemyBullet, transform.position, transform.rotation);
			TrooperAttackSpeed = SetTrooperAttackSpeed;
		} else if (BruiserAttackSpeed <= 0 && gameObject.name == "Bruiser") {
			Instantiate (EnemyShotgun2, transform.position, transform.rotation);
			BruiserAttackSpeed = SetBruiserAttackSpeed;
		} else if (BoomerAttackSpeed <= 0 && gameObject.name == "Boomer") {
			Instantiate (EnemyBullet, transform.position, transform.rotation);
			BoomerAttackSpeed = SetBoomerAttackSpeed;
		}
	}
	}