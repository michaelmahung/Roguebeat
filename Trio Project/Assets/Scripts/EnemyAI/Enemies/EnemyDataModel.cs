using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataModel : MonoBehaviour {


public GameObject EnemyBullet;
public GameObject EnemyShotgun2;
public GameObject BombPrefab;
public Transform Hero;
public float MoveSpeed = 5.0f;
public float TrooperHP;
public float BruiserHP;
public float BoomerHP;



	// Use this for initialization
	void Start ()
	{
		EnemyBullet = GameObject.Find ("Enemy_Fire_1");
		EnemyShotgun2 = GameObject.Find ("Enemy_Fire_Shotgun");
		BombPrefab = GameObject.Find ("Bomb_Shot");
		if (gameObject.name == "Trooper") {
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
