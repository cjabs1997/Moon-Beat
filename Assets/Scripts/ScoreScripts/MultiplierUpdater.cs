using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierUpdater : MonoBehaviour
{
    [Tooltip("ScriptableObject that holds the current score.")]
    public FloatVariable multiplierVariable;
    [Tooltip("The text that comes before the multiplier, '____ <multiplier> ...'.")]
    public string prefaceText;
    [Tooltip("The text that comes after the multiplier, '... <multiplier> ____'.")]
    public string suffixText;

    TMPro.TextMeshProUGUI m_TextMeshProUGUI;

    private void Awake()
    {
        m_TextMeshProUGUI = this.GetComponent<TMPro.TextMeshProUGUI>();
    }


    public void UpdateMultiplier()
    {
        m_TextMeshProUGUI.text = prefaceText + " " + multiplierVariable.Value.ToString("0.0") + suffixText;
    }
}
