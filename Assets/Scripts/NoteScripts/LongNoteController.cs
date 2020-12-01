using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteController : MonoBehaviour
{
    public float noteSpeed;
    public GameEvent longNoteHitEvent;
    public GameEvent missNoteEvent;

    private LineRenderer m_LineRenderer;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private Vector3 initialPosition;
    private float graphValue;
    private bool noteFinished = false;

    private float startTime;
    private float length;
    private float buttonY;

    private void Awake()
    {
        m_LineRenderer = this.GetComponentInChildren<LineRenderer>();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Animator = this.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonY = collision.gameObject.transform.position.y;
    }





    /// <summary>
    /// When the note is selected by the button.
    /// </summary>
    public void HitNote()
    {
        m_SpriteRenderer.enabled = false;
        /*
        Debug.Log("ORIGINAL LINE LENGTH: " + m_LineRenderer.GetPosition(1).y);
        float distanceToButton = this.gameObject.transform.position.y - buttonY;
        m_LineRenderer.SetPosition(1, new Vector3(m_LineRenderer.GetPosition(1).x, m_LineRenderer.GetPosition(1).y - Mathf.Abs(distanceToButton), m_LineRenderer.GetPosition(1).z));
        Debug.Log("NEW LINE LENGTH: " + m_LineRenderer.GetPosition(1).y);
        */
        m_Animator.speed = 0;
        StartCoroutine(ShrinkRoutine());
    }
    
    /// <summary>
    /// If the button is released before the note is finished.
    /// </summary>
    public void EarlyRelease()
    {
        // Separated this out just in case there is more we want to do here...

        DestroyNote();
    }

    public void InitNote(float length)
    {
        this.length = length;
        Vector3 linePos = new Vector3(0f, (length * noteSpeed) - 0.25f, 0f);

        m_LineRenderer.SetPosition(1, linePos);
        initialPosition = m_LineRenderer.GetPosition(1);
    }

    /// <summary>
    /// Getter for determining whether the note was finished or not.
    /// </summary>
    /// <returns>True if note was finished, false otherwise.</returns>
    public bool GetNoteFinished()
    {
        return noteFinished;
    }

    /// <summary>
    /// If the note is not pressed.
    /// </summary>
    public void MissNote()
    {
        missNoteEvent.Raise();
        DestroyNote();
    }

    /// <summary>
    /// Destroys the parent GameObject, thus destroying the note.
    /// </summary>
    private void DestroyNote()
    {
        Destroy(this.transform.parent.gameObject);
    }

    /// <summary>
    /// Handles shrinking. Currently not working o.O
    /// </summary>
    /// <returns></returns>
    IEnumerator ShrinkRoutine()
    {
        startTime = Time.time;

        while (true)
        {
            float distancedCovered = (Time.time - startTime) * noteSpeed;
            float fractionOfJourny = distancedCovered / initialPosition.y;
            Vector3 newVector = Vector3.Lerp(initialPosition, Vector3.zero, fractionOfJourny);
            m_LineRenderer.SetPosition(1, newVector);

            if (newVector == Vector3.zero)
                break;

            yield return new WaitForEndOfFrame();
        }

        noteFinished = true;
        longNoteHitEvent.Raise();
        Destroy(this.transform.parent.gameObject);
    }
}
