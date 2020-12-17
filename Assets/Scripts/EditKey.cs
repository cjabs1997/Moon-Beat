using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditKey : MonoBehaviour
{
    [Range(0, 6)]
    [Tooltip("Which button this scripts corresponds to (from left to right starting at 0). Should be unique from the other buttons.")]
    public int buttonNum;
    public ControlsManager controlsManager;

    private bool editable = false;
    private TMPro.TextMeshProUGUI m_TextMeshProUGUI;
    private float startingFontSize;
    private string buttonText = "";


    private void Awake()
    {
        m_TextMeshProUGUI = this.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        startingFontSize = m_TextMeshProUGUI.fontSize;
    }

    private void OnGUI()
    {
        Event e = Event.current;

        if(e.isKey && editable)
        {
            SetNewKey(e.keyCode);

            controlsManager.UpdateButton(buttonNum, e.keyCode);

            editable = false;
            controlsManager.keyEditing = false;
        }
    }

    private void SetNewKey(KeyCode k)
    {
        m_TextMeshProUGUI.fontSize = startingFontSize;
        buttonText = k.ToString();
        m_TextMeshProUGUI.text = buttonText;
    }


    public void EnableEdit()
    {
        // Quick way of seeing if another button is being edited. If so then don't make a new button editable
        if(controlsManager.keyEditing == true)
        {
            return;
        }

        m_TextMeshProUGUI.fontSize = 16f;
        editable = true;
        controlsManager.keyEditing = true;
        m_TextMeshProUGUI.text = "Press any key";
    }

    public void VoidButton()
    {
        m_TextMeshProUGUI.text = "";
    }

    public void InitButton(KeyCode keyCode)
    {
        buttonText = keyCode.ToString();
        m_TextMeshProUGUI.text = buttonText;
    }

    public void ResetText()
    {
        m_TextMeshProUGUI.fontSize = startingFontSize;
        buttonText = controlsManager.config.buttonControls[buttonNum].ToString();
        m_TextMeshProUGUI.text = buttonText;
    }
}
