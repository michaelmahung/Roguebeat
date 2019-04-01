using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioInfo
{
    public AudioClip clip;
    public float minPitch;
    public float maxPitch;
    public float volume;
    public bool randomizePitch;
    public bool loop;
}

public class WeaponAudio : MonoBehaviour
{
    AudioSource MySource;

    [SerializeField] AudioInfo currentInfo;

    void Start()
    {
        MySource = GetComponent<AudioSource>();
        MySource.playOnAwake = false;
        MySource.loop = false;
    }

    public void PlayClip(AudioInfo audio)
    {
        if (audio.clip != currentInfo.clip)
        {
            MySource.clip = audio.clip;
            MySource.volume = audio.volume;
            MySource.loop = audio.loop;
        }

        if (audio.randomizePitch)
        {
            MySource.pitch = Random.Range(audio.minPitch, audio.maxPitch);
        }

        if (MySource != null)
        {
            MySource.Play();
        } 

        currentInfo = audio;
    }

    public void StopAudio()
    {
        if (MySource != null)
        {
            MySource.Stop();
        }
    }
}
