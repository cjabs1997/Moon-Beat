using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    [Tooltip("ScriptableObject that holds the current score.")]
    public IntVariable scoreVariable;
    [Tooltip("The text that comes before the score, '____ <score>'.")]
    public string prefaceText;

    TMPro.TextMeshProUGUI m_TextMeshProUGUI;

    private void Awake()
    {
        m_TextMeshProUGUI = this.GetComponent<TMPro.TextMeshProUGUI>();
    }


    public void UpdateScore()
    {
        m_TextMeshProUGUI.text = prefaceText + " " + scoreVariable.Value;
    }
}
