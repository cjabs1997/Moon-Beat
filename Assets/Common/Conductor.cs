﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{  
    // delegate for beat event
    public delegate void BeatEvent(float songPositionInBeats);
    public static event BeatEvent OnBeat;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    private float previousSongPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    private float delay;

    private bool delayedPlaying;

    // inverse the sample rate for calculation efficiency
    private float inverseSampleRate;

    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {  

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        songBpm = songBpm < 0 ? 0 : songBpm; // quick negative reset

        // Automatically calculate song BPM if not specified
        // Automatic calculation can be off, so try and rely on manual input
        if(songBpm == 0){
            songBpm = UniBpmAnalyzer.AnalyzeBpm(musicSource.clip);
            if (songBpm < 0)
            {
                Debug.LogError("AudioClip is null.");
                return;
            }
        }

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        // Get inverse sample rate for calculating songPosition
        inverseSampleRate = (float)1/musicSource.clip.frequency;

    }

    // Update is called once per frame
    void Update()
    {   
        // delay calculations
        if(delayedPlaying && musicSource.time == 0)
        {
            this.delay = musicSource.clip.frequency * ((float)  AudioSettings.dspTime - dspSongTime);
        }else{
            delayedPlaying = false;
        }

        //determine how many seconds since the song started over the sample rate
        songPosition = ((float)musicSource.timeSamples + this.delay) * inverseSampleRate;

        previousSongPositionInBeats = songPositionInBeats;

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        // determine if new beat event
        if(previousSongPositionInBeats != songPositionInBeats)
        {
            if(OnBeat != null)
                OnBeat(songPositionInBeats);
        }

        isPlaying = musicSource.isPlaying;

    }

    public void setBPM(int bpm)
    {
        this.songBpm = bpm;
        this.secPerBeat = 60f / this.songBpm;
    }

    public void startMusic()
    {
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        delayedPlaying = true;
        musicSource.PlayDelayed(firstBeatOffset * secPerBeat);
    }
}