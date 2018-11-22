using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataModel : MonoBehaviour , IDamageable<float> {

protected GameObject[] EnemyWeapons;
protected GameObject EnemyShotgun2;
protected GameObject BombPrefab;
public Transform Hero;
public float MoveSpeed;
public float EnemyHealth;
public float EnemyAttackSpeed;
public MyStruct[] EnemyWeaponTypes;
[System.Serializable]
public struct MyStruct {
[HideInInspector]
public string name;
public GameObject Weapon;
}
public Rigidbody EnemyBody;
public Vector3 PlayerLocation;



	// Use this for initialization
	public void Start ()
	{
		Hero = GameObject.FindGameObjectWithTag("Player").transform;
		PlayerLocation = gameObject.transform.forward;
	EnemyWeapons = Resources.LoadAll ("/Prefabs/EnemyWeapons") as GameObject[];
		EnemyShotgun2 = GameObject.Find ("Enemy_Fire_Shotgun");
		BombPrefab = GameObject.Find ("Bomb_Shot");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Damage (float damage)
	{
		EnemyHealth -= damage;
		if (EnemyHealth <= 0) {
			Destroy (gameObject);
		}
	}


}
