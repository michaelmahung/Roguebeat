using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct MixerGroupSettings
{
    public AudioSource Source;
    public AudioMixerGroup Group;
}

[System.Serializable]
public struct AudioClipSettings
{
    public AudioClip Clip;
    public float CurrentClipTime;
    public bool ShouldLoop;
}


public class DynamicMusic : MonoBehaviour
{
    //I need to be able to know several things in order for this system to run smoothly.
    /*
     * What my current active track is
     * what my track to transition to is
     * Have to be notified as to when tracks change
     * Have to make sure that only one track is "active" at any given time
     * Have to be able to play transitions between tracks
     * MUST BE PERFORMANT
     * */

    [Header("Audio Sets")]
    [SerializeField] MixerGroupSettings mixerGroup1;
    [SerializeField] MixerGroupSettings mixerGroup2;

    [SerializeField] MixerGroupSettings currentMixerGroup;
    [SerializeField] MixerGroupSettings transMixerGroup;

    [SerializeField] AudioClipSettings currentClipSettings;

    [Header("Audio Settings")]
    [Range(-80f, -60f)] [SerializeField] float minVolume = -80;
    [Range(-5, 0)] [SerializeField] float maxVolume = 0;
    [Range(0, 60)] [SerializeField] float minVolumeOffset = 40;

    [SerializeField] float timer = 0;
    [SerializeField] float musicFadeTime = 0;
    [SerializeField] bool transitioning;
    [SerializeField] int musicIndex = 0;

    [SerializeField] AudioClipSettings[] allClipSettings;

    void Start()
    {
        PlayerHealth.PlayerDamaged += CheckTransition;

        mixerGroup1.Group.audioMixer.SetFloat(mixerGroup1.Group.name + "Volume", maxVolume);
        mixerGroup2.Group.audioMixer.SetFloat(mixerGroup2.Group.name + "Volume", minVolume);

        currentMixerGroup = mixerGroup1;
        transMixerGroup = mixerGroup2;

        currentClipSettings = allClipSettings[0];
    }

    void ResetAudio()
    {
        currentMixerGroup.Source.Stop();
        transMixerGroup.Source.Stop();

        for (int i = 0; i < allClipSettings.Length; i++)
        {
            allClipSettings[i].CurrentClipTime = 0;
        }

        mixerGroup1.Source.clip = allClipSettings[0].Clip;
        mixerGroup2.Source.clip = allClipSettings[1].Clip;

        currentMixerGroup = mixerGroup1;
        currentMixerGroup.Source.loop = currentClipSettings.ShouldLoop;

        mixerGroup1.Group.audioMixer.SetFloat(mixerGroup1.Group.name + "Volume", maxVolume);
        mixerGroup2.Group.audioMixer.SetFloat(mixerGroup2.Group.name + "Volume", minVolume);

        currentMixerGroup.Source.Play();
    }

    void Update()
    {
        if (currentClipSettings.CurrentClipTime < currentClipSettings.Clip.length && !currentClipSettings.ShouldLoop)
        {
            currentClipSettings.CurrentClipTime += Time.deltaTime;
        }
        else
        {
            NextTrack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0;
            transitioning = true;
            currentMixerGroup.Source.loop = false;
            transMixerGroup.Source.Play();
        }

        if (timer < 1 && transitioning)
        {
            FadeTracks(mixerGroup2, mixerGroup1);

            if (timer > 1)
            {
                timer = 1;
                transitioning = false;

            }

            timer += Time.deltaTime / musicFadeTime;
        }
    }

    void NextTrack()
    {
        Debug.Log("Switching to next track");
        musicIndex++;
        currentMixerGroup.Source.clip = allClipSettings[musicIndex].Clip;
        currentClipSettings.Clip = currentMixerGroup.Source.clip;

        currentMixerGroup.Source.Play();
    }

    void FadeTracks(MixerGroupSettings increase, MixerGroupSettings decrease)
    {
        //increase.Group.audioMixer.SetFloat(increase.Group.name + "Volume", maxVolume);
        //decrease.Group.audioMixer.SetFloat(decrease.Group.name + "Volume", minVolume);
        increase.Group.audioMixer.SetFloat(increase.Group.name + "Volume", Mathf.Lerp(minVolume + minVolumeOffset, maxVolume, timer));
        decrease.Group.audioMixer.SetFloat(decrease.Group.name + "Volume", Mathf.Lerp(maxVolume, minVolume, timer));
    }

    void CheckTransition()
    {
        if (GameManager.Instance.PlayerHealthRef.HealthPercent >= PlayerStats.HIGHHPMIN)
        {
            Debug.Log("Player has high HP");
        }

        else if (GameManager.Instance.PlayerHealthRef.HealthPercent < PlayerStats.HIGHHPMIN && GameManager.Instance.PlayerHealthRef.HealthPercent >= PlayerStats.MEDHPMIN)
        {
            Debug.Log("Player has medium HP");
        }

        else if (GameManager.Instance.PlayerHealthRef.HealthPercent < PlayerStats.MEDHPMIN)
        {
            Debug.Log("Player has low HP");
        }

        else
        {
            Debug.Log("Howd you get here");
        }
    }
}
