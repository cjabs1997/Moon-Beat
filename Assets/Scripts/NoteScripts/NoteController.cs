using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>
/// Handles basic functionality of notes.
/// 
/// - How many points a note is worth
/// - Destroying the notes
/// </summary>
public class NoteController : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameEvent noteHitEvent;
    public GameEvent noteMissEvent;

    private float buttonY;
    private CinemachineImpulseSource m_CinemachineImpulseSource;

    private void Awake()
    {
        m_CinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
    }

    /*
     * // If we want the notes to rotate :)
    private void Update()
    {
        //this.gameObject.transform.Rotate(Vector3.forward * 1080f * Time.deltaTime);
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonY = collision.gameObject.transform.position.y;
    }

    /// <summary>
    /// For when the note is successfully hit by the player.
    /// </summary>
    public void HitNote()
    {
        noteHitEvent.Raise();
        GameObject.Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        m_CinemachineImpulseSource.GenerateImpulse();
        //Debug.Log("DISTANCE: " + (this.gameObject.transform.position.y - buttonY));
        Destroy(this.transform.parent.gameObject);
    }

    /// <summary>
    /// For when the note is missed by the player. Called by the animator on the Note
    /// </summary>
    public void MissNote()
    {
        noteMissEvent.Raise();

        // If want anything to happen to the screen or anything do it here...

        Destroy(this.transform.parent.gameObject);
    }
}
