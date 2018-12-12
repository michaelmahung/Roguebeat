using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : EnemyEngagement {

	// Use this for initialization
	new void Start () {
		base.Start2(); // Causes the base parent class (EnemyEngagement) to run it's "Start2" as part of this Start function
		MoveSpeed = 3.0f; // assigns base enemy move speed per Boomer
		EnemyHealth = 25.0f; // assigns base enemy health per Boomer
		EnemyAttackSpeed = 2.0f; // assigns base enemy attack speed per Boomer
		WeaponValue = 0; // assigns int value to 0 in reading the EnemyWeapons gameobject array in grandparent class EnemyDataModel, which reads from EnemyWeapons Folder
        KillPoints = 50;
	}
	
	// Update is called once per frame
	void Update ()
	{
		SeePlayer (); // calls SeePlayer function in parent class
		float closestDistance = Mathf.Infinity;
		GameObject closestPlayerShot = null;
		GameObject[] AllPlayerBullets = (GameObject[])GameObject.FindGameObjectsWithTag ("PlayerBaseShot");
		foreach (GameObject s in AllPlayerBullets) {

			if (s.name != this.name) {

				float distance = (s.transform.position - this.transform.position).sqrMagnitude;
				if (distance < closestDistance) {
					closestDistance = distance;
					closestPlayerShot = s;
				}
			}
		}

		if (AllPlayerBullets.Length > 0) { 
			Debug.DrawLine (this.transform.position, closestPlayerShot.transform.position);
		}

	}

	private void FixedUpdate(){
		ChasePlayer(); //calls ChasePlayer function in parent class

	}
}
