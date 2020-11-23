using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEditor;

public class Chart
{   

    private string chart;
    private Dictionary<string, Dictionary<string, List<string>>> chartSections;
    private static Regex sectionMatcher = new Regex(@"\[(?<key>.+?)\][\n\r]+\{[\n\r]+(?<values>.+?)[\n\r]+\}", RegexOptions.Singleline | RegexOptions.Compiled);
    private static Regex sectionParse = new Regex(@"(.+?)=(.+)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
    private readonly Dictionary<string, string> metadata;
    private List<List<Tuple<float,int,string,float>>> playList; 
    private static string notesSection = "ExpertSingle";
    private static string metadataSection = "Song";

    public Chart(TextAsset chartAsset)
    {   
        this.chartSections = new Dictionary<string, Dictionary<string, List<string>>>();
        this.chart = chartAsset.text;
        parseChart(this.chart);
        this.metadata = convertMetadata();
        this.playList = genNotesToPlay();
    }

    private void parseChart(string chart){
        MatchCollection matches = sectionMatcher.Matches(chart);

        if(matches.Count > 0)
            foreach(Match m in matches)
                chartSections.Add(m.Groups[1].Value.Trim(), extractSectionValues(m.Groups[2].Value));
    }

    private Dictionary<string, List<string>> extractSectionValues(string input)
    {
        Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
        MatchCollection matches = sectionParse.Matches(input);

        if(matches.Count > 0)
            foreach(Match m in matches)
            {
                string key = m.Groups[1].Value.Trim();
                string value = m.Groups[2].Value.Trim();
                // There's probably a simpler way of doing this, but don't know c# enough
                if(!results.ContainsKey(key)){
                    List<string> t = new List<string>();
                    t.Add(value);
                    results.Add(key, t);
                }else{
                    results[key].Add(value);
                }
            }
        return results;
    }

    // public functions

    public List<Tuple<float,int,string,float>> getNextNotes()
    {
        // just needs to serve the next note info; no need to keep tempo
        if(this.playList.Count == 0)
            return null;
        List<Tuple<float,int,string,float>> last = new List<Tuple<float,int,string,float>>(this.playList[this.playList.Count-1]);
        this.playList.RemoveAt(this.playList.Count-1);
        return last;
    }

    public Dictionary<string, string> getMetadata()
    {
        return metadata;
    }

    // utility functions
    private Dictionary<string, string> convertMetadata()
    {
        Dictionary<string, string> returnCopy = new Dictionary<string, string>();
        Dictionary<string, List<string>> metaSection = chartSections[metadataSection];
        foreach(string key in metaSection.Keys){
            returnCopy.Add(key, metaSection[key][0]);
        }
        return returnCopy;
    }

    
    private List<List<Tuple<float,int,string,float>>> genNotesToPlay()
    {
        List<List<Tuple<float,int,string,float>>> res = new List<List<Tuple<float,int,string,float>>>();

        foreach(string beat in chartSections[notesSection].Keys)
        {
            float beatPos = float.Parse(beat) / float.Parse(this.metadata["Resolution"]);
            List<Tuple<float,int,string,float>> multiNotes = new List<Tuple<float,int,string,float>>();
            foreach(string note in chartSections[notesSection][beat])
            {
                string[] noteInfo = note.Split();
                string noteType = noteInfo[0];
                int noteInt;
                float length;
                if(noteType.Equals("N")){
                    noteInt = Int32.Parse(noteInfo[1]);
                    length = float.Parse(noteInfo[2]) / float.Parse(this.metadata["Resolution"]);
                }else{
                    continue;
                }
                multiNotes.Add( new Tuple<float,int,string,float>(beatPos, noteInt, noteType, length) );
            }
            if(multiNotes.Count > 0)
                res.Insert(0, multiNotes);
        }

        return res;
    }

}
