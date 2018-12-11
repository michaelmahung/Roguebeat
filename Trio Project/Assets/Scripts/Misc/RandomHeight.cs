using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour {

    public int MaxHeight = 50;
    public int MinHeight = 40;

	void Start ()
    {
        //Start this thing from a random height (within bounds)
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinHeight, MaxHeight), gameObject.transform.position.z);
	}

}
