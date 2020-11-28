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

    private float buttonY;
    private CinemachineImpulseSource m_CinemachineImpulseSource;

    private void Awake()
    {
        m_CinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        //this.gameObject.transform.Rotate(Vector3.forward * 1080f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonY = collision.gameObject.transform.position.y;
    }

    public void DestroyNote()
    {
        noteHitEvent.Raise();
        GameObject.Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        m_CinemachineImpulseSource.GenerateImpulse();
        Debug.Log("DISTANCE: " + (this.gameObject.transform.position.y - buttonY));
        Destroy(this.transform.parent.gameObject);
    }
}
