using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic_Alt : MonoBehaviour
{
    public static AudioSource ActiveAudioSource;
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] float clipVolume = 0.5f;
    int currentClipIndex = 0;
    int clipIndexMax;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        clipIndexMax = audioSources.Length - 1;

        foreach (AudioSource source in audioSources)
        {
            source.pitch = 1;
            source.volume = 0;
            source.loop = true;
            source.Play();
        }

        ActiveAudioSource = audioSources[0];
        audioSources[0].volume = clipVolume;
    }

    void Start()
    {
        PlayerHealth.PlayerKilled += ResetMusic;
        PlayerHealth.PlayerDamaged += CheckPlayerHealth;
    }

    void CheckPlayerHealth()
    {
        if (GameManager.Instance.PlayerHealthRef.HealthPercent < PlayerStats.HIGHHPMIN && GameManager.Instance.PlayerHealthRef.HealthPercent >= PlayerStats.MEDHPMIN)
        {
            ChangeMainTrack(1);
        }

        else if (GameManager.Instance.PlayerHealthRef.HealthPercent < PlayerStats.MEDHPMIN && GameManager.Instance.PlayerHealthRef.HealthPercent >= PlayerStats.LOWHPMIN)
        {
            ChangeMainTrack(2);
        }

        else if (GameManager.Instance.PlayerHealthRef.HealthPercent <= PlayerStats.LOWHPMIN)
        {
            ChangeMainTrack(3);
        }

        else
        {
            ChangeMainTrack(0);
        }
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentClipIndex++;
            ChangeMainTrack(currentClipIndex);
        }
    }*/

    void ChangeMainTrack (int index)
    {
        if (index > clipIndexMax)
        {
            index = clipIndexMax;
        }

        if (index != currentClipIndex)
        {
            currentClipIndex = index;
        }

        if (audioSources[index].volume != clipVolume)
        {
            for (int i = 0; i <= clipIndexMax; i++)
            {
                if (audioSources[i] != audioSources[index])
                {
                    audioSources[i].volume = 0;
                }
                else
                {
                    ActiveAudioSource = audioSources[i];
                    audioSources[i].volume = clipVolume;
                }
            }
        }
    }

    void ResetMusic()
    {
        currentClipIndex = 0;

        foreach (AudioSource source in audioSources)
        {
            source.volume = 0;
        }

        ActiveAudioSource = audioSources[0];
        audioSources[0].volume = clipVolume;
    }
}
