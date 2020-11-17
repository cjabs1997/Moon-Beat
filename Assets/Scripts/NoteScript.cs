using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float pointValue;

    // How much of the note is in a button. 
    // Used to determine how many points should we awared for sucessful presses.
    private int areaHit = 0;
    // How many colliders are on the object. 
    //Used to find the percentage of the object that was hit to determine how many points should be awared. 
    private int totalArea = 0;

    private void Awake()
    {
        totalArea = this.GetComponents<Collider2D>().Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        areaHit = Mathf.Min(areaHit + 1, totalArea); // Just ensure we never go above the maximum amount.
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        areaHit = Mathf.Max(areaHit - 1, 0); // Just ensure we never go below 0.
    }

    public void DestroyNote()
    {
        Debug.Log("AREA HIT: " + areaHit);
    }
}
