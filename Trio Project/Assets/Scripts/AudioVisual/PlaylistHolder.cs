using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //Make this component use an audiosource
public class PlaylistHolder : MonoBehaviour
{
    [System.Serializable] //allow this to be seen in the inspector
    public struct MusicTest //Create a custom data type that contains an int for level and an array of audioclips.
    {
        public int level; //Heres where we tell the data structure what we want in it
        public AudioClip[] songs;
    }


    public MusicTest[] music = new MusicTest[3]; //Make an array of the struct
    public List<AudioClip> audioClips = new List<AudioClip>(); //Create a list (so we can edit its elements during runtime)

    private AudioSource audioSource; //Create a private audiosource (we know one has to exist because we require one at the top of the class)
    bool stop; //A bool that will be used to decide if we will continue to test our function.
    [SerializeField]
    private int currentLevel; //Create an int to represent what our current level is

    private int songValue; //Variable to track our current song
    private MusicTest currentStruct; //Variable to track our current struct


    private void Start()
    {
        songValue = 1;
        currentLevel = 1;
        audioSource = GetComponent<AudioSource>(); //Grab a referene to the audiosource
        StartCoroutine(Test()); //Start our test coroutine
    }

    private void Update()
    {
        //Listen for input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousSong();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSong();
        }
    }

    //This function will append our audioclip list depending on the level we feed to it.
    private int AppendList(int level)
    {
        int trueLevel = level - 1;
        try //Attempt to do all of this, if there is an error at any point, go to the catch section
        {
            currentStruct = music[trueLevel]; //Current struct is the struct we assigned for the current level.
            audioClips.Clear(); //Clear our list

            //Heres what this loop does:
            //Set an int to 0
            //While i is less than the amount of songs within the struct elements music array.
            //Increase i by 1, and add the song to our musicclip list.
            //We need to make sure that we subtract 1 because the array will start from 0, therefore level 1 will grab the music[0] value.

            //For example: while i = 0, we will go to music[0] - or the first set of structs and grab the amount of songs within that struct.
            //For each song in the array, add the song to our list.
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
            stop = true;
        }

        return level;
    }

    IEnumerator Test() //This coroutine will run at the start
    {
        if (!stop)
        {
            //If we havent been flagged with any errors, continue with the coroutine.
            Debug.LogFormat("Making music list for level {0}.", currentLevel);
            AppendList(currentLevel); //Run this function for whatever the current level is.
            yield return new WaitForSeconds(5); //Wait for 5 seconds
            currentLevel += 1;
            StartCoroutine("Test");
        } else
        {
            //Otherwise stop the coroutine from running.
            StopAllCoroutines();
            Debug.Log("Stopping loop");
        }
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


}
