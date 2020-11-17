using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles basic functionality of notes.
/// 
/// - How many points a note is worth
/// - Destroying the notes
/// </summary>
public class NoteController : MonoBehaviour
{
    public GameObject explosionEffect;

    private float buttonY;

    private void Awake()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonY = collision.gameObject.transform.position.y;
    }

    public void DestroyNote()
    {
        GameObject.Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        Debug.Log("DISTANCE: " + (this.gameObject.transform.position.y - buttonY));
        Destroy(this.gameObject);
    }
}
