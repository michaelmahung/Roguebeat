using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour {

    public float minScale = 1;
    public float maxScale = 5;

	void Start () {
        //Give this thing a random scale (within bounds)
        gameObject.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
    }
	
}
