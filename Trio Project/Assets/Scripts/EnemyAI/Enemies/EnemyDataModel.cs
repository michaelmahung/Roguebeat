using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Base Parent class for enemies and their behavior. Inherits from Interfaces script.
/// </summary>
public class EnemyDataModel : MonoBehaviour , IDamageable<float>, ITrackRooms {


[HideInInspector]
public GameObject[] EnemyWeapons;

protected Transform Hero; // Transform variable used to acquire the player.

[Tooltip("Movement Speed Of Enemy")]
public float MoveSpeed; // base variable for all enemy movespeeds; is uniquely set on specific enemy class
[Tooltip("Amount Of Enemy Health")]
public float EnemyHealth; // base variable for all enemy health; is uniquely set on specific enemy class
[Tooltip("Rate Of Fire Of Enemy")]
public float EnemyAttackSpeed; // base variable for all enemy attack speeds; is uniquely set on specific enemy class

protected bool IsFiring; // bool created to assist a Coroutine of enemy fire and wait time before firing again, used in Enemy Engagement Class
public int WeaponValue; // int to allow selection of enemy weapon prefabs within the EnemyWeapons array, used in Enemy Engagement Class
public string CurrentRoom { get; set; }

//Handles Color change on damage to enemy
private Color EnemyBaseColor;
[Tooltip("Time Length Of Damage Visual")]
public float hurtDuration = 0.5f; // duration of hurt visual
[Tooltip("Incremental Change Rate For Damage Visual")]
public float smoothColor = 0.10f; //rate of color change for hurt visual
//Handles Color change on damage to enemy

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
        EnemyBaseColor = gameObject.GetComponent<Renderer>().material.color;      
        print (GetComponent<Renderer>().material.color);                                                             //for (int i = 0; i < EnemyWeapons.Length; i++) { ********** Code for testing purposes to read EnemyWeapons folder contents
                                                                               //}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Damage (float damage) // function on enemies to read damage from fire from player, reads Damage from Interfaces script.
	{
		EnemyHealth -= damage;
		StartCoroutine(LerpColor()); // begin lerping color to show damage to enemy
		if (EnemyHealth <= 0) {
		enemyDeath();
		}
	}

	public void enemyDeath ()
	{
        GameManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Kills);
        Destroy(gameObject);
    }


    // Coroutine that increments and lerps from red to the enemy original color when enemy takes damage.
    IEnumerator LerpColor ()
	{
		float progress = 0; //instance float created on start of coroutine 
		float increment = smoothColor / hurtDuration; //instance float created on start of coroutine
		while (progress < 1) 
		{
		gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, EnemyBaseColor, progress);
		progress += increment; //add to Float progress by an amount from smoothColor divided by hurtDuration
		yield return new WaitForSeconds(smoothColor);
		}
	}
}
