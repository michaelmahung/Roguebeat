using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour {

    public int MaxHeight = 60;
    public int MinHeight = 50;

	void Start ()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinHeight, MaxHeight), gameObject.transform.position.z);
	}

}
