using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour {

    [SerializeField] private int MaxHeight = 50;
    [SerializeField] private int MinHeight = 40;

	void Start ()
    {
        //Start this thing from a random height (within bounds)
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinHeight, MaxHeight), gameObject.transform.position.z);
	}

}
