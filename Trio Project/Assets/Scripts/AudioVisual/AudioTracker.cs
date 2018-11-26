using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioTracker : MonoBehaviour {
    AudioSource _AS;
    //List<AudioClip> songList;
    public static float[] _samples = new float[512];
    public AudioClip[] levelOne;
    public AudioClip[] levelTwo;
    public AudioClip[] levelThree;
    public AudioClip[] levelFour;
    public AudioClip[] levelFive;
    public AudioClip[] levelSix;
    //public AudioClip[] levelList;
    //[SerializeField]
    private ArrayList currentArray;
    [SerializeField]
    private int level = 1;
    [SerializeField]
    int TotalLevels = 6;

	// Use this for initialization
	void Start () {
        AudioSource();
        ArrayList currentArray = new ArrayList { levelOne, levelTwo, levelThree, levelFour, levelFive, levelSix };
        int songArrays = currentArray.Count;
        for (int i = 0; i < songArrays; i++)
        {
            int[] songs = currentArray[i] as int[];
            int items = songs.Length;
            for (int j = 0; j < items; j++)
            {
                int song = songs[j];
                print(song);
            }
        }
        print(currentArray);
        //List<AudioClip> songList = new List<AudioClip>(6);
        //songList.Add(s1);
        //selectionNumber = 
        _AS = GetComponent<AudioSource>();
        //_AS.clip = s1;
        //UNCOMMENT ME WHEN START WORK AGAIN!!! _AS.clip = songArrays(0)[0];
        _AS.Play(0);
        print("current level is " + level);

    }

	// Update is called once per frame
	void Update () {
        GetSpectrumAudiosSource();
        //AudioSource();
        if (Input.GetKeyDown(KeyCode.P))
        {
            level++;
            
            if (level > 6)
            {
                level = 6;
            }
            print("current level is " + level);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           /* songNumber++;
            if (songNumber > songList.Count)
            {
                songNumber = 1;
            }
            //audioData(songList[songNumber]).Play;
            _AS.clip = songList[songNumber];
            _AS.Play(0);*/
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            /*songNumber--;
            if (songNumber < 1)
            {
                songNumber = songList.Count;
            }
            _AS.clip = songList[songNumber];
            _AS.Play(0);*/
        }
    }

    void GetSpectrumAudiosSource()
    {
        _AS.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void AudioSource()
    {
        //_AS.clip = songList[songNumber];
        //_AS.Play(0);
        /*if (song1 == true && oneAdded == false)
        {
            songList.Add(s1);
            
            oneAdded = true;
        }*/
        /*if (song2 == true && twoAdded == false)
        {
            songList.Add(s2);
            twoAdded = true;
           
        }
        if (song3 == true && threeAdded)
        {
            songList.Add(s3);
            threeAdded = true;
        }
        if (song4 == true && fourAdded == false)
        {
            songList.Add(s4);
            fourAdded = true;
        }
        if (song5 == true && fiveAdded == false)
        {
            songList.Add(s5);
            fiveAdded = true;
        }
        if (song6 == true && sixAdded == false)
        {
            songList.Add(s6);
            sixAdded = true;
        }*/
    }

    void addToArray(AudioClip song)
    {
        songList.Add(song);
    }

    public void PlayMusic()
    {
        if (_AS.isPlaying)
        {
            return;
        }
        //selectionNumber
    }
}
