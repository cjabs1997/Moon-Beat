using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class Composer : MonoBehaviour
{   
    // Needs to be able to get and play 4 (or n) blank beats before the start of playing notes and conductor
    // Needs to be able to load chart , sync note creation with conductor, and parse chart (delegate to another class?)


    // Get chart needing to be played
    // public Target chartLocation;
    public TextAsset chartFile;
    // Conductor class used to keep time
    public Conductor conductor;

    // amount of beats delayed for note spawn look ahead
    public int beatDelay;

    private Chart chart;

    private int resolution;

    private List<Tuple<float,int,string,float>> curBeatNotes;

    private float songPositionInBeats;
    // private Array chartArray;

    // Start is called before the first frame update
    void Start()
    {   
        
        this.chart = new Chart(chartFile);
        this.conductor = GetComponent<Conductor>();
        Conductor.OnBeat += beatEvent;

        // this.conductor.startMusic();
    }

    // Update is called once per frame
    void Update()
    {   
        // get next note if necessary
        if(this.curBeatNotes == null)
        {
            this.curBeatNotes = this.chart.getNextNotes();
        }
        //spawn note if you have notes
        if(this.curBeatNotes != null && this.curBeatNotes[0].Item1 < this.songPositionInBeats)
        {
            generateNotes(this.curBeatNotes);
            this.curBeatNotes = null;
        }
    }

    void OnDestroy()
    {
        Conductor.OnBeat -= beatEvent;
    }

    private void generateNotes(List<Tuple<float,int,string,float>> notes)
    {
        for(int i = 0; i < notes.Count; ++i)
            Debug.Log("Generated - songpos: " + notes[i].Item1 + " noteInt: " + notes[i].Item2 + " noteType: " + notes[i].Item3 + " noteLength: " + notes[i].Item4);
    }

    private void beatEvent(float songPositionInBeats)
    {
        this.songPositionInBeats = songPositionInBeats;
        Debug.Log("Called beat event");
    }

}
