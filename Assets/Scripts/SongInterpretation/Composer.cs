using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class Composer : MonoBehaviour
{   
    // Generate note event
    public delegate void GenNote(Tuple<float,int,string,float> noteInfo);
    public static event GenNote GenNoteCallback;

    // public Target chartLocation;
    public TextAsset chartFile;

    // Conductor class used to keep time
    public Conductor conductor;

    public bool autoStartSong;

    private Chart chart;

    private int resolution;

    private List<Tuple<float,int,string,float>> curBeatNotes;

    private float songPositionInBeats;

    // Start is called before the first frame update
    void Awake()
    {   
        
        this.chart = new Chart(chartFile);
        this.conductor = GetComponent<Conductor>();
        Conductor.OnBeat += beatEvent;
        Chart.BPMChangeCallback += updateBeat;

        var chartBPM = this.chart.getBPMAt(0);
        if(chartBPM >= 0)
            this.conductor.setBPM(chartBPM);
        else
            Debug.Log("WTF");
    }

    private void Start()
    {
        if (autoStartSong)
            StartCoroutine(SongDelay());
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
        if(this.curBeatNotes != null && this.curBeatNotes[0].Item1 < this.songPositionInBeats && conductor.isPlaying)
        {
            generateNotes(this.curBeatNotes);
            this.curBeatNotes = null;
        }

    }

    void OnDestroy()
    {
        Conductor.OnBeat -= beatEvent;
    }

    public void startSong()
    {
        this.conductor.startMusic();
    }

    public void stopSong()
    {
        this.conductor.stopMusic();
    }

    private void updateBeat(float bpm)
    {
        this.conductor.setBPM(bpm);
        Debug.Log("BPM UPDATED TO " + bpm);
    }

    private void generateNotes(List<Tuple<float,int,string,float>> notes)
    {
        // Tuple< songposition, note integer, note type, note length >
        for(int i = 0; i < notes.Count; ++i)
        {
            Tuple<float,int,string,float> note = notes[i];
            // Debug.Log("Generated - songpos: " + note.Item1 + " noteInt: " + note.Item2 + " noteType: " + note.Item3 + " noteLength: " + note.Item4);
            if(GenNoteCallback != null)
                GenNoteCallback(note);
        }
            
    }

    private void beatEvent(float songPositionInBeats)
    {
        this.songPositionInBeats = songPositionInBeats;
    }

    IEnumerator SongDelay()
    {
        yield return new WaitForSeconds(2f);

        this.conductor.startMusic();
    }
}
