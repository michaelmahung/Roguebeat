using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagement : EnemyDataModel {


	// Use this for initialization
	public void Start2 () {
	base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator FireWeapon ()
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

