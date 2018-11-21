using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataModel : MonoBehaviour , IDamageable<float> {

public GameObject[] EnemyWeapons;
protected GameObject EnemyShotgun2;
protected GameObject BombPrefab;
public Transform Hero;
public float MoveSpeed = 5.0f;
protected int TrooperHP = 10;
protected int BruiserHP = 16;
protected int BoomerHP = 20;
public float EnemyHealth;



	// Use this for initialization
	public void Start ()
	{
		Hero = GameObject.FindGameObjectWithTag("Player").transform;
		print(Hero.name); // Locate player via tag to look at and chase
	EnemyWeapons = Resources.LoadAll ("/Prefabs/EnemyWeapons") as GameObject[];
		EnemyShotgun2 = GameObject.Find ("Enemy_Fire_Shotgun");
		//print(EnemyShotgun2.name);
		BombPrefab = GameObject.Find ("Bomb_Shot");
		print("In");
		
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
