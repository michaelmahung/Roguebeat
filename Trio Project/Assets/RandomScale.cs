using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour {

    public float minScale;
    public float maxScale;

	void Start () {
        gameObject.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
    }
	
}
