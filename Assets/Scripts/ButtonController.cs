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

    private void Awake()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        if(Input.GetKeyDown(activationKey) && selectedNote)
        {
            selectedNote.GetComponent<NoteController>().DestroyNote();
        }
        else if(Input.GetKey(activationKey))
        {
            m_SpriteRenderer.color = pressedColor;
        }
        else if(Input.GetKeyUp(activationKey))
        {
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
