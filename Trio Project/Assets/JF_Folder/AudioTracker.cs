using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioTracker : MonoBehaviour {
    AudioSource _AS;
    //AudioSource audioData;
    //List <AudioClip> songList = new List<AudioClip>(6);
    //public List<int> songNumber;
    List<AudioClip> songList;
   // public int selectionNumber;
    //public AudioClip[] songList;
    //public AudioClip music;
    public AudioClip s1;
    public AudioClip s2;
    public AudioClip s3;
    public AudioClip s4;
    public AudioClip s5;
    public AudioClip s6;
    public bool song1 = true;
    public bool song2 = false;
    public bool song3 = false;
    public bool song4 = false;
    public bool song5 = false;
    public bool song6 = false;
    bool oneAdded;
    bool twoAdded;
    bool threeAdded;
    bool fourAdded;
    bool fiveAdded;
    bool sixAdded;
    public int songNumber;
    public static float[] _samples = new float[512];
    

	// Use this for initialization
	void Start () {
        AudioSource();
        List<AudioClip> songList = new List<AudioClip>(6);
        songList.Add(s1);
        //selectionNumber = 
        _AS = GetComponent<AudioSource>();
        _AS.clip = s1;
        _AS.Play(0);
       // songNumber = new List<int>(songList.Count);
        //for (int i = 0; i<songList.Count; i++)
      //  {
       //     songNumber.Add(1);
       // }
        //songNumber = 1;
        //songList = s1;
        //_AS.clip = s1;// songList[songNumber];
        //_AS.Play(0);

       
	}

	// Update is called once per frame
	void Update () {
        GetSpectrumAudiosSource();
        //AudioSource();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            songNumber++;
            if (songNumber > songList.Count)
            {
                songNumber = 1;
            }
            //audioData(songList[songNumber]).Play;
            _AS.clip = songList[songNumber];
            _AS.Play(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            songNumber--;
            if (songNumber < 1)
            {
                songNumber = songList.Count;
            }
            _AS.clip = songList[songNumber];
            _AS.Play(0);
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
        if (song2 == true && twoAdded == false)
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
        }
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
