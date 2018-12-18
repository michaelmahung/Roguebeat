using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaylistHolder : MonoBehaviour
{
    [System.Serializable] 
    public struct MusicTest
    {
        public int level;
        public AudioClip[] songs;
    }


    public MusicTest[] music = new MusicTest[3];
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    [SerializeField]
    private int currentLevel;

    private int songValue;
    private MusicTest currentStruct;
    public float[] _samples = new float[1024];
    //public Queue<int> _samples;

    private void Start()
    {
        songValue = 0;
        currentLevel = 1;
        audioSource = GetComponent<AudioSource>();
        AppendList(currentLevel);
        //_samples = new Queue<int>();
    }

    private void Update()
    {
        GetSpectrumAudioSource();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousSong();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSong();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeLevel();
        }
    }

    private int AppendList(int level)
    {
        int trueLevel = level - 1;

        try
        {
            currentStruct = music[trueLevel];
            audioClips.Clear();

            for (int i = 0; i < music[trueLevel].songs.Length; i++)
            {
                audioClips.Add(music[trueLevel].songs[i]);
            }

            audioSource.clip = music[trueLevel].songs[songValue];
            audioSource.Play();
        }
        catch
        {
            Debug.LogError("Cannot append list, stopping loop.");
        }

        return level;
    }

     private void ChangeLevel()
     {
        currentLevel++;

        if (currentLevel > 6)
        {
            currentLevel = 6;
        }

        print("current level is " + currentLevel);
        AppendList(currentLevel);
     }

    private void NextSong()
    {
        if (songValue < currentStruct.songs.Length - 1)
        {
            songValue++;
        } else
        {
            songValue = 0;
        }

        ChangeSong();
    }

    private void PreviousSong()
    {
        if (songValue > 0)
        {
            songValue--;
        } else
        {
            songValue = currentStruct.songs.Length - 1;
        }

        ChangeSong();
    }

    private void ChangeSong()
    {
        audioSource.Stop();
        audioSource.clip = currentStruct.songs[songValue];
        audioSource.Play();
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
