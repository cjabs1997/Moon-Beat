using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode activationKey;
    [Tooltip("Color for the button in a resting state.")]
    public Color defaultColor;
    [Tooltip("Color for the button while it is being pressed.")]
    public Color pressedColor;

    private SpriteRenderer m_SpriteRenderer;
    private GameObject selectedNote;
    private Animator m_Animator;

    private void Awake()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        // If the activationKey was hit this frame
        if(Input.GetKeyDown(activationKey))
        {
            m_Animator.SetBool("Selected", true);
            m_SpriteRenderer.color = pressedColor;

            // If there is a note within our collider destroy it
            if (selectedNote)
            {
                selectedNote.GetComponent<NoteController>().HitNote();
            }
        }
        else if(Input.GetKeyUp(activationKey))
        {
            if (m_Animator) { m_Animator.SetBool("Selected", false); }
            m_SpriteRenderer.color = defaultColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        selectedNote = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        selectedNote = null;
    }
}
