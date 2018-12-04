using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Base Parent class for enemies and their behavior. Inherits from Interfaces script.
/// </summary>
public class EnemyDataModel : MonoBehaviour , IDamageable<float> {

[HideInInspector]// Hide this array of EnemyWeapons from the Inspector.
public GameObject[] EnemyWeapons;
public Transform Hero; // Transform variable used to acquire the player.
public float MoveSpeed; // base variable for all enemy movespeeds; is uniquely set on specific enemy class
public float EnemyHealth; // base variable for all enemy health; is uniquely set on specific enemy class
public float EnemyAttackSpeed; // base variable for all enemy attack speeds; is uniquely set on specific enemy class
public bool IsFiring; // bool created to assist a Coroutine of enemy fire and wait time before firing again, used in Enemy Engagement Class
public int WeaponValue; // int to allow selection of enemy weapon prefabs within the EnemyWeapons array, used in Enemy Engagement Class
public LevelDoors KillingForDoors;

/*public MyStruct[] EnemyWeaponTypes; ****************************Struct Usage(Mike)
[System.Serializable]
public struct MyStruct {
[HideInInspector]
public string name;
public GameObject Weapon;
public int Health;
public float TimeToDie;
}
*/

	// INITIAL START FOR ENEMY DATA MODEL CLASS > ENEMY ENGAGEMENT CLASS > (NAMED) ENEMY CLASS
	public void Start ()
	{
		Hero = GameObject.FindGameObjectWithTag ("Player").transform; // Finds the player via Player tag 
		EnemyWeapons = Resources.LoadAll<GameObject> ("Prefabs/EnemyWeapons"); // Assigns the entire contents of the folder EnemyWeapons in the Resources folder to the EnemyWeapons array.
		//for (int i = 0; i < EnemyWeapons.Length; i++) { ********** Code for testing purposes to read EnemyWeapons folder contents
		//}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Damage (float damage) // function on enemies to read damage from fire from player, reads Damage from Interfaces script.
	{
		EnemyHealth -= damage;
		if (EnemyHealth <= 0) {
		enemyDeath();
		}
	}

	public void enemyDeath ()
	{
	KillingForDoors.AddKills();
	Destroy (gameObject);

	}
}
