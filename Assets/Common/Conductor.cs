using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{  

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    private int previousSongPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    // inverse the sample rate for calculation efficiency
    private float inverseSampleRate;

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
            Debug.Log("songBpm: " + songBpm);
        }

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        // Get inverse sample rate for calculating songPosition
        inverseSampleRate = (float)1/musicSource.clip.frequency;

        //Start the music
        // musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started over the sample rate
        songPosition = (float)musicSource.timeSamples * inverseSampleRate;

        //determine how many beats since the song started
        // previousSongPositionInBeats = songPositionInBeats;
        songPositionInBeats = songPosition / secPerBeat;

    }

    public void start()
    {
        musicSource.Play();
    }
}
