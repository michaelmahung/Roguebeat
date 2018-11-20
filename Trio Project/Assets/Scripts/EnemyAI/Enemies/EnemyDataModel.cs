using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataModel : MonoBehaviour {

public GameObject[] EnemyWeapons;
protected GameObject EnemyShotgun2;
protected GameObject BombPrefab;
protected Transform Hero;
protected float MoveSpeed = 5.0f;
protected int TrooperHP = 10;
protected int BruiserHP = 16;
protected int BoomerHP = 20;



	// Use this for initialization
	void Start ()
	{
	//EnemyWeapons = new GameObject[2];
	EnemyWeapons = Resources.LoadAll ("/Prefabs/EnemyWeapons") as GameObject[];
		EnemyShotgun2 = GameObject.Find ("Enemy_Fire_Shotgun");
		BombPrefab = GameObject.Find ("Bomb_Shot");
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	}