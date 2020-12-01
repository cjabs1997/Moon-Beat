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

    [Header("Events")]
    public GameEvent missPressEvent;

    private SpriteRenderer m_SpriteRenderer;
    private GameObject selectedNote;
    private Animator m_Animator;
    private Collider2D m_Collider2D;
    private LongNoteController selectedLongNote;

    private void Awake()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Animator = this.GetComponent<Animator>();
        m_Collider2D = this.GetComponent<Collider2D>();
    }


    void Update()
    {
        // If the activationKey was hit this frame
        if(Input.GetKeyDown(activationKey))
        {
            m_Animator.SetBool("Selected", true);
            m_SpriteRenderer.color = pressedColor;
            int numHits = 0;
            RaycastHit2D[] hits = new RaycastHit2D[5];
            numHits = m_Collider2D.Cast(Vector2.zero, hits);

            // We didn't find any notes in our range when we hit the button
            if (numHits == 0)
            {
                missPressEvent.Raise();
            }
            else // We hit some notes!
            {
                // Iterate over all the notes we hit and call the appropriate functions.
                for (int i = 0; i < numHits; ++i)
                {
                    GameObject note = hits[i].transform.gameObject;
                    // Regular note was hit
                    if(note.TryGetComponent<NoteController>(out NoteController noteController))
                    {
                        noteController.HitNote();
                    }
                    // Long note was hit, want to make sure the distance isn't too extreme
                    else if (Vector3.Distance(this.gameObject.transform.position, note.transform.position) <= 0.3f)
                    {
                        selectedLongNote = note.GetComponentInChildren<LongNoteController>();
                        selectedLongNote.HitNote();
                    }
                }
            }
        }
        else if(Input.GetKeyUp(activationKey))
        {
            if (m_Animator) { m_Animator.SetBool("Selected", false); }
            m_SpriteRenderer.color = defaultColor;

            // If we release the key before the long note is finished playing we missed the note
            if(selectedLongNote != null && !selectedLongNote.GetNoteFinished())
            {
                selectedLongNote.EarlyRelease();
                selectedLongNote = null;
                missPressEvent.Raise();
            }
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
