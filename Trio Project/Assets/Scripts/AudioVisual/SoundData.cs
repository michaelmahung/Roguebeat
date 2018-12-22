using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundData {

    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 0.3f;

    [Range(.1f, 2f)]
    public float pitch = 1f;

    public float minPitch = 0.75f;
    public float maxPitch = 1.5f;

    public bool loop;

    public bool randomizePitch;

    [HideInInspector]
    public AudioSource audioSource;

}
