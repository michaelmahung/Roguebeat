using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour {

    public int MaxHeight;
    public int MinHeight;

	void Start ()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinHeight, MaxHeight), gameObject.transform.position.z);
	}

}
