using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : MonoBehaviour {

public float initialSize = 1.0f;
public Vector3 maxSize;
public float expandSpeed = 1.0f;
public bool expand;


private Vector3 targetScale;

	// Use this for initialization
	void Start () {
	expand = true;
	maxSize = new Vector3(13,13,13);
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	if (transform.localScale == maxSize){
	expand = false;
	}

		if (expand == true) {
			transform.localScale += new Vector3 (1, 1, 1);
			if (transform.localScale == new Vector3 (12, 12, 12)) {

				Destroy (gameObject);
			}
		}
	}
	}
