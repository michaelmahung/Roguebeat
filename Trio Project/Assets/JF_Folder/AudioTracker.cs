using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioTracker : MonoBehaviour {
    AudioSource _AS;
    public static float[] _samples = new float[512];
	// Use this for initialization
	void Start () {
        _AS = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
        GetSpectrumAudiosSource();
    }

    void GetSpectrumAudiosSource()
    {
        _AS.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
