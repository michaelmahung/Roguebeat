using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShot : MonoBehaviour {
public float BombLife = 1.0f;
public float BombSpeed = 20.0f;
public GameObject BigBoom;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		BombLife -= Time.deltaTime;
		if (BombLife <= 0) {
		callExplosion();
		}

		if (BombLife > 0) {
			transform.position += transform.forward * BombSpeed * Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Wall") {
			callExplosion ();
		}
	}

	void callExplosion ()
	{
		Instantiate (BigBoom, transform.position, transform.rotation);
			Destroy (gameObject);
			}
			}