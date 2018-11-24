using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailThickness : MonoBehaviour 
{

    [Range(0,7)]
    [Tooltip("The frequency band the trail will React (Smaller number = lower range)")]
    public int band;
    [Range(0,1)]
    public float minThickness = 0;
    [Range(0, 2)]
    public float maxThickness = 1f;

    TrailRenderer tr;

	void Start ()
    {
        tr = GetComponent<TrailRenderer>();
	}
	
	void LateUpdate ()
    {
        tr.widthMultiplier = (AudioPeer._audioBandBuffer[band] * (maxThickness - minThickness)) + minThickness;
	}
}
