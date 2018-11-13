using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {


public GameObject EnemyFire1;
public Transform Hero;
public float MoveSpeed = 5.0f;



	// Use this for initialization
	void Start () {
	EnemyFire1 = GameObject.Find("Enemy_Fire_1");
	Hero = GameObject.FindGameObjectWithTag("Player").transform;



	//HeroTarget = Hero.GetComponent<Transform>;
		
	}
	
	// Update is called once per frame
	void Update () {

	transform.LookAt(Hero);
	transform.position += transform.forward*MoveSpeed*Time.deltaTime;
		
	}
}
