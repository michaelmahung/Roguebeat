﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : MonoBehaviour {

public float initialSize;
public float maxSize;
public float expandSpeed = 1.0f;
public float Explosion;
public bool Changing;
public bool Unchanged;


private Vector3 targetScale;

	// Use this for initialization
	void Start () {
	Changing = false;
	Unchanged = true;
	initialSize = gameObject.transform.localScale.x;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Changing == false && Unchanged == true){
			Explosion += Time.deltaTime * 0.9f;
			float Grow = Mathf.Lerp (initialSize, initialSize * 6.0f, Explosion);
			gameObject.transform.localScale = new Vector3 (Grow, Grow, Grow);
			maxSize = gameObject.transform.localScale.x;
			if (Explosion >= 1.0f) {
				Explosion = 1.0f;
				Unchanged = false;
				Changing = true;

			}
		}

		if (Changing == true && Unchanged == false) {
			Explosion -= Time.deltaTime * 0.9f;
			float small = Mathf.Lerp (maxSize / 1.5f, maxSize, Explosion);
			gameObject.transform.localScale = new Vector3 (small, small, small);
			if (Explosion <= 0.0f) {
				Explosion = 0.0f;
				Changing = false;
				Destroy(gameObject);

			}
		}
	}
}
