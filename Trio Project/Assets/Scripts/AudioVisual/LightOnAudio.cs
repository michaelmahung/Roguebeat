using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour
{

    public int Band = 4;
    public float MinIntensity, MaxIntensity;

    Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        //Read the intensity of the selected audio band, do stuff.
        _light.intensity = (AudioPeer._audioBandBuffer[Band] * (MaxIntensity - MinIntensity)) + MinIntensity;
    }
}
