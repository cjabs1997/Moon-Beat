using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAnimEvents : MonoBehaviour
{
    private NoteController m_NoteController;

    private void Awake()
    {
        m_NoteController = this.GetComponentInChildren<NoteController>();
    }


    public void MissedNote()
    {
        m_NoteController.MissNote();
    }

    public void MissedLongNote()
    {
        this.GetComponentInChildren<LongNoteController>().MissNote();
    }

    public void HideLineRenderer()
    {
        this.GetComponentInChildren<LineRenderer>().enabled = false;
    }
}
