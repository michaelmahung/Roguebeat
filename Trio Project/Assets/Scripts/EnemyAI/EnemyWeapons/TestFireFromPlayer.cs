using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFireFromPlayer : MonoBehaviour {
private GameObject PlayerBullet;
public GameObject EnemyBullet;

	// Use this for initialization
	void Start () {
		//PlayerBullet = GameObject.Find("Enemy_Fire_1");
		EnemyBullet = GameObject.Find ("Enemy_Fire_1");
		
	}
	
	// Update is called once per frame
	void Update () {

	if (Input.GetKeyDown(KeyCode.Mouse0)){
	Instantiate( EnemyBullet, transform.position, transform.rotation);
	print ("I'm firing!");
	}
	}
	}