using System;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour {

    public static SFXManager Instance;

    [Header("Sound Information")]

    public SoundData[] Sounds;

    public AudioSource AudioPlayer;

    [HideInInspector]
    public AudioLowPassFilter Filter;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetComponents();
            return;
        }
        Destroy(gameObject);
    }

    private void Start ()
    {

		foreach (SoundData s in Sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
	}

    public void PlaySound(string name)
    {
        SoundData s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("No sound with name " + name + " found.");
            return;
        }

        if (s.randomizePitch)
        {
            float random = UnityEngine.Random.Range(s.minPitch, s.maxPitch);
            s.audioSource.pitch = random;
        }
        s.audioSource.Play();
    }

    public void StopSound(string name)
    {
        SoundData s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("No sound with name " + name + " found.");
            return;
        }

        if (s.audioSource != null)
        {
            s.audioSource.Stop();
        }
    }

    private void SetComponents()
    {
        Filter = AudioPlayer.GetComponent<AudioLowPassFilter>();
        Filter.enabled = false;
        Filter.cutoffFrequency = 400;
    }
}
