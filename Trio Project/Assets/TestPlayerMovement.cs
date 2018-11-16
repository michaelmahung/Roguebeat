using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour {
public float MovementSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	if(Input.GetKeyDown(KeyCode.W)){
	transform.position += transform.forward * MovementSpeed * Time.deltaTime;
	}

		if(Input.GetKeyDown(KeyCode.A)){

			transform.position -= transform.right * MovementSpeed * Time.deltaTime;
	}

		if(Input.GetKeyDown(KeyCode.S)){

			transform.position -= transform.forward * MovementSpeed * Time.deltaTime;
	}

		if(Input.GetKeyDown(KeyCode.D)){

			transform.position += transform.right * MovementSpeed * Time.deltaTime;
	}
		
	}
}
