using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailThickness : MonoBehaviour {

    public int band;
    public float minThickness = 0, maxThickness = 1f;
    TrailRenderer tr;

	void Start ()
    {
        tr = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tr.widthMultiplier = (AudioPeer._audioBandBuffer[band] * (maxThickness - minThickness)) + minThickness;
	}
}
