using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

public float BulletLife = 6.0f;
public float BulletSpeed = 50.0f;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		BulletLife -= Time.deltaTime;
		if (BulletLife <= 0) {
		Destroy(gameObject);
		}

		if (BulletLife > 0) {
			transform.position += transform.forward * BulletSpeed * Time.deltaTime;
		}
	}
}


