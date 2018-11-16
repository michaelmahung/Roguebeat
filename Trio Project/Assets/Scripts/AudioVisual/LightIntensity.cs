using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightIntensity : MonoBehaviour {


    //This is an old script and can probably be safely deleted.

    Light pulseLight;
    float intensityChange = 0.05f;

	// Use this for initialization
	void Start ()
    {
        pulseLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.Q) && pulseLight.intensity < .4f)
        {
            pulseLight.intensity += intensityChange;
        }

        if (Input.GetKey(KeyCode.E) && pulseLight.intensity > 0f)
        {
            pulseLight.intensity -= intensityChange;
        }
    }
}
