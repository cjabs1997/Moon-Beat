using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Info")]
    [Tooltip("IntVariable holding the current score.")]
    public IntVariable score;
    [Tooltip("How much hitting a note is worth. Adjusted by the multiplier before being added to the score. Ideally this should be greater than 10.")]
    public int baseNoteValue;
    [Tooltip("GameEvent raised when the score is updated.")]
    public GameEvent updateScoreEvent;

    [Header("Multiplier Info")]
    [Tooltip("FloatVariable holding the current multiplier.")]
    public FloatVariable currMultiplier;
    [Tooltip("The maximum value the multiplier can reach.")]
    public float maxMultiplier;

    [Tooltip("How much the multiplier increases when scaleHitNumNotes amount of notes are hit.")]
    public float multiplierScaleHit;
    [Tooltip("How many notes need to be hit in a row before the multiplier increases.")]
    public int scaleHitNumNotes;

    [Tooltip("How much the multiplier goes down when scaleMissNumNotes amount of notes are missed. Should be a positive number.")]
    public float multiplierScaleMiss;
    [Tooltip("How many notes need to be missed in a row before the multiplier goes down.")]
    public int scaleMissNumNotes;

    [Tooltip("GameEvent raised when the multiplier value is updated.")]
    public GameEvent updateMultiplierEvent;


    private int numNotesHitInARow = 0;
    private int numNotesMissedInARow = 0;

    private void Start()
    {
        ResetValues();
    }

    /// <summary>
    /// Helper function for reseting values. Called on Awake but can be used to manually reset values without reloading scene.
    /// </summary>
    private void ResetValues()
    {
        score.ResetValue();
        currMultiplier.Value = currMultiplier.defaultValue;

        updateScoreEvent.Raise();
    }

    /// <summary>
    /// Called when the player misses a note. Determines if the multiplier should go down or not and raises the appropriate event if so.
    /// </summary>
    public void NoteMissed()
    {
        numNotesHitInARow = 0;
        ++numNotesMissedInARow;

        numNotesMissedInARow = numNotesMissedInARow % scaleMissNumNotes;

        if (numNotesMissedInARow == 0)
        {
            currMultiplier.Value = Mathf.Max(currMultiplier.Value - multiplierScaleMiss, 1);
            updateMultiplierEvent.Raise();
        }
    }

    /// <summary>
    /// Called when a player hits a note. Upddtes the score and determines if the mutliplier should increase. Fires off appropriate events
    /// </summary>
    public void NoteHit()
    {
        numNotesMissedInARow = 0;
        ++numNotesHitInARow;

        score.Value = (int)(score.Value + (baseNoteValue * currMultiplier.Value));

        numNotesHitInARow = numNotesHitInARow % scaleHitNumNotes;

        if(numNotesHitInARow == 0)
        {
            currMultiplier.Value = Mathf.Min(multiplierScaleHit + currMultiplier.Value, maxMultiplier);
            updateMultiplierEvent.Raise();
        }

        updateScoreEvent.Raise();
    }
}
