using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {


public GameObject EnemyBullet;
public GameObject EnemyShotgun2;
public Transform Hero;
public float MoveSpeed = 5.0f;



	// Use this for initialization
	void Start () {
	EnemyBullet = GameObject.Find("Enemy_Fire_1");
	EnemyShotgun2 = GameObject.Find("Enemy_Fire_Shotgun");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
