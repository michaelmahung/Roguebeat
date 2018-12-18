using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trooper : EnemyEngagement{


	// Use this for initialization
	new void Start ()
	{

		base.Start2 (); // Causes the base parent class (EnemyEngagement) to run it's "Start2" as part of this Start function 
		MoveSpeed = 10.0f; // assigns base enemy move speed per Trooper
		EnemyHealth = 10.0f; // assigns base enemy health per Trooper
		EnemyAttackSpeed = 0.1f; // assigns base enemy attack speed per Trooper
		WeaponValue = 1; // assigns int value to 1 in reading the EnemyWeapons gameobject array in grandparent class EnemyDataModel, which reads from EnemyWeapons Folder
        KillPoints = 10;
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
				if (distance < 1.0f) {
				StartCoroutine(EnemyDodge());
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

	IEnumerator EnemyDodge ()
	{
		int DodgeChance = Random.Range (0, 100);
		if (DodgeChance < 30) {
			int DirectionDodge = Random.Range (1, 3);
			print (DirectionDodge);
			var CurrentPosition = transform.position;
			if (DirectionDodge == 1) {
				transform.position = Vector3.Lerp(CurrentPosition, (CurrentPosition + (transform.right * 2)), 0.05f);
				yield return new WaitForSeconds(5.0f);
			}
			if (DirectionDodge == 2) {
				transform.position = Vector3.Lerp(CurrentPosition, (CurrentPosition + (transform.right / 2)), 0.05f);
			}
			yield return new WaitForSeconds(5.0f);
		}



	}
}
