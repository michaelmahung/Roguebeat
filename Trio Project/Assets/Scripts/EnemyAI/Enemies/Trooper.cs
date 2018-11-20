using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trooper : EnemyEngagement{


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision other){

	if (other.gameObject.tag == "PlayerBaseShot" && gameObject.name == "Trooper") {
			TrooperHP -= 2;
			}
			}
}
