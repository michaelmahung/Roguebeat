using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Engagement Class for enemies. Reads base variable data stored in parent class EnemyDataModel. Stores functions to be called in specific enemy classes.
/// </summary>
public class EnemyEngagement : EnemyDataModel {


	// Not a REAL start function, serves as typical function to be called from specific enemy classes. 
	public void Start2 () {
	base.Start(); // call to its parent class, EnemyDataModel, to ensure it runs it's full Start function first, to ensure it's Start calls reach the unique enemy classes.
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator FireWeapon () // 
	{
		yield return new WaitForSeconds(EnemyAttackSpeed);
		Instantiate (EnemyWeapons[WeaponValue], transform.position, transform.rotation);
		StartCoroutine(FireWeapon());
	}

	public void SeePlayer ()
	{
		transform.LookAt (Hero); // cause enemies to look at Player
		if (IsFiring == false) {
			IsFiring = true;
		StartCoroutine(FireWeapon());
		}
	}

	public void ChasePlayer ()
	{
	transform.position += transform.forward*MoveSpeed*Time.deltaTime;
	}
}

