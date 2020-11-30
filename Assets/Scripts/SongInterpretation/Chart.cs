using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEditor;

public class Chart
{   

    // TODO Extract BPM from chart file and event emit it
    // TODO Call event emit everytime there's a bpm change

    // bpm 
    private List<Tuple<float, float>> bpms;

    // chart parsing
    private string chart;
    private Dictionary<string, Dictionary<string, List<string>>> chartSections;

    // regex
    private static Regex sectionMatcher = new Regex(@"\[(?<key>.+?)\][\n\r]+\{[\n\r]+(?<values>.+?)[\n\r]+\}", RegexOptions.Singleline | RegexOptions.Compiled);
    private static Regex sectionParse = new Regex(@"(.+?)=(.+)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

    // data holders
    private readonly Dictionary<string, string> metadata;
    private List<List<Tuple<float,int,string,float>>> playList; 


    // variables to read sections
    private static string bpmSection = "SyncTrack";
    private static string notesSection = "ExpertSingle"; // hardcoded for now
    private static string metadataSection = "Song";

    #region Init Functions

    public Chart(TextAsset chartAsset)
    {   
        this.chartSections = new Dictionary<string, Dictionary<string, List<string>>>();
        this.chart = chartAsset.text;
        parseChart(this.chart);
        this.metadata = convertMetadata();
        this.playList = genNotesToPlay();
        this.bpms = convertBpm();
    }

    private void parseChart(string chart){
        var matches = sectionMatcher.Matches(chart);

        if(matches.Count > 0)
            foreach(Match m in matches)
                chartSections.Add(m.Groups[1].Value.Trim(), extractSectionValues(m.Groups[2].Value));
    }

    private Dictionary<string, List<string>> extractSectionValues(string input)
    {
        var results = new Dictionary<string, List<string>>();
        MatchCollection matches = sectionParse.Matches(input);

        if(matches.Count > 0)
            foreach(Match m in matches)
            {
                string key = m.Groups[1].Value.Trim();
                string value = m.Groups[2].Value.Trim();
                // There's probably a simpler way of doing this, but don't know c# enough
                if(!results.ContainsKey(key)){
                    var t = new List<string>();
                    t.Add(value);
                    results.Add(key, t);
                }else{
                    results[key].Add(value);
                }
            }
        return results;
    }

    #endregion

    #region Public Functions

    public List<Tuple<float,int,string,float>> getNextNotes()
    {
        // just needs to serve the next note info; no need to keep tempo
        if(this.playList.Count == 0)
            return null;
        var last = new List<Tuple<float,int,string,float>>(this.playList[this.playList.Count-1]);

        /*
         * 
         * Handling BPM changes elsewhere now...
        // emit event if bpm changes
        var bpm = getBPMAt(last[0].Item1);
        //Debug.Log(bpm);
        if(BPMChangeCallback != null &&  bpm >= 0)
        {
            BPMChangeCallback(bpm);
        }
        */

        // remove note from playlist
        this.playList.RemoveAt(this.playList.Count-1);
        return last;
    }

    public List<Tuple<float, float>> GetNextBeat()
    {
        // Out of bpm changes, do nothing
        if (bpms.Count == 0)
            return null;

        // I am hella dumb and could not get the one liner for this to work :)
        var last = new List<Tuple<float, float>>();
        Tuple<float, float> nextBeat = bpms[bpms.Count - 1];

        last.Add(nextBeat);

        bpms.RemoveAt(bpms.Count - 1);
        return last;
    }

    public Dictionary<string, string> getMetadata()
    {
        return metadata;
    }

    public float getBPMAt(float time)
    {
        if(this.bpms.Count > 0 && time >= this.bpms[this.bpms.Count - 1].Item1)
        {
            var res = this.bpms[this.bpms.Count - 1].Item2;
            this.bpms.RemoveAt(this.bpms.Count - 1);
            return res;
        }
        return -1;
    }

    #endregion

    #region Utility Functions

    private Dictionary<string, string> convertMetadata()
    {
        var returnCopy = new Dictionary<string, string>();
        var metaSection = chartSections[metadataSection];
        foreach(string key in metaSection.Keys){
            returnCopy.Add(key, metaSection[key][0]);
        }
        return returnCopy;
    }

    
    private List<List<Tuple<float,int,string,float>>> genNotesToPlay()
    {
        // result to create to return
        var res = new List<List<Tuple<float,int,string,float>>>(); // Tuple< songposition, note integer, note type, note length >

        foreach(var beat in chartSections[notesSection].Keys)
        {
            // convert the beat postion
            float beatPos = float.Parse(beat) / float.Parse(this.metadata["Resolution"]);
            var multiNotes = new List<Tuple<float,int,string,float>>();
            foreach(var note in chartSections[notesSection][beat])
            {
                string[] noteInfo = note.Split();
                var noteType = noteInfo[0];
                int noteInt;
                float length;
                if(noteType.Equals("N")){
                    noteInt = Int32.Parse(noteInfo[1]);
                    length = float.Parse(noteInfo[2]) / float.Parse(this.metadata["Resolution"]);
                    multiNotes.Add( new Tuple<float,int,string,float>(beatPos, noteInt, noteType, length) );
                }
            }
            if(multiNotes.Count > 0)
                res.Insert(0, multiNotes);
        }

        return res;
    }
    

    /// <summary>
    /// Add all the BPM changes to a list in a reverse order (first change is at the end of the list).
    /// </summary>
    /// <returns>Item1 is when the BPM should change (in beats?), Item2 is what the BPM should change to.</returns>
    private List<Tuple<float, float>> convertBpm()
    {
        var bpms = new List<Tuple<float, float>>();
        foreach(var bpmPos in chartSections[bpmSection].Keys)
        {   
            foreach(var v in chartSections[bpmSection][bpmPos]){
                var val = v.Split();
                if(val.Length > 0 && val[0] == "B")
                {  
                    // bpm needs to be scaled down by 1000
                    float bpm = float.Parse(val[1]) / 1000;
                    bpms.Insert(0, new Tuple<float, float>( float.Parse(bpmPos) / float.Parse(this.metadata["Resolution"]), bpm));
                }
            }
        }
        /*
        Debug.Log(bpms[0].Item1);
        Debug.Log(bpms[0].Item2);

        Debug.Log(bpms[1].Item1);
        Debug.Log(bpms[1].Item2);
        

        Debug.Log(bpms[2].Item1);
        Debug.Log(bpms[2].Item2);
        
        Debug.Log(bpms[3].Item1);
        Debug.Log(bpms[3].Item2);
        */

        return bpms;
    }

    #endregion
}
