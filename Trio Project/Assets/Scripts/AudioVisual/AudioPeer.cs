using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    float[] _freqBand = new float[8];
    float[] _bandBuffer = new float[8];
    //We want to create buffers that will smooth out the falling of the bands. In order to do this we will create a band buffer.
    float[] _bufferDecrease = new float[8];
    //Next, we want to create values between 0 and 1 so we can assign certain action to our game.
    //To do this we will get the highest possible value and store it in a new float.
    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    public static float _Amplitude, _AmplitudeBuffer;
    float _AmplitudeHighest;



    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }

        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
                _freqBandHighest[i] = _freqBand[i];
            {
                _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
                _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
            }
        }

    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    //Well create a new function that will compart if the current frequency band is greater than or less than the band buffer.
    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bufferDecrease[g] = (_bandBuffer[g] - _freqBand[g]) / 8;
                _bandBuffer[g] -= _bufferDecrease[g];
            }
        }

    }

    void MakeFrequencyBands()
    {
        /*
         * 22050 / 512 = 43 hz per sample
         * 20 - 60 hz
         250 - 500 hz
         2000 - 4000 hz
         4000 - 6000 hz
         6000 - 20000 hz
    
         These above ranges are the different levels of audible sound in hertz. The low ranges are larger/bass sounds while the higher ranges are almost like sirens.

         Because our song will probably have a maximum hz value of 22050, we will divide that by 512 to get 43 hz per sample.
         We will then use 43 as a baseline variable to create our 8 bands.
         Below, we will split the hertz ranges up into minimum and maximum brackets so that we can make an 8-bar visualiser that will have generally equal 
         movements between them. With a 512 bar visualiser, the upper ranges generally do not get moved so we will instead simply group them together.
         The number on the left is the bar assigned to the range, and the number to the right will be the hz range minimum and maximum. 
         As long as the hz range is detected within that range, something will be triggered.

         * 0 - 2 = 86 hz
         * 1 - 4 = 172 hz - 87-258 
         * 2 - 8 = 344 hz - 259-602
         * 3 - 16 = 688 hz - 603-1290
         * 4 - 32 = 1376 hz - 1291-2666
         * 5 - 64 = 2752 hz - 2667-5418
         * 6 - 128 = 5504 hz - 5419-10922
         * 7 - 256 = 11008 hz - 10923-21930
         * 
         * After adding all of the previous samples, we end up with 510 instead of 512, so we can simply have the final script add 2 samples to finalize our numbers.
         * 510
         */
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            //We will create a counter to keep track of the amount of samples.
            //As stated earlier, for the last sample size we need to add 2.
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;
        }
    }
}
