using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composer : MonoBehaviour
{   
    // Needs to be able to get and play 4 (or n) blank beats before the start of playing notes and conductor
    // Needs to be able to load chart , sync note creation with conductor, and parse chart (delegate to another class?)


    // Get chart needing to be played
    // public <type> chart;

    // Conductor class used to keep time
    public Conductor conductor;

    // amount of beats delayed for note spawn look ahead
    public int beatDelay;

    // private Array chartArray;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GetComponent<Conductor>();

        // chartArray = loadChart();

        Conductor.OnBeat += beatEvent;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        Conductor.OnBeat -= beatEvent;
    }

    private void generateNote()
    {

    }

    private void loadChart()
    {
        // dummy values for now; have to parse chart
        // return new Array.CreateInstance(typeof((Int32,Int32)), (0,2), (1,3), (2,4));
    }

    private void beatEvent(float songPositionInBeats)
    {
        Debug.Log("Beat " + songPositionInBeats);
    }

}
