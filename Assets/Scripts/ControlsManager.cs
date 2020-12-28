using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public List<EditKey> buttons;
    public ControlConfig config;
    public bool keyEditing = false;

    private CanvasGroup m_CanvasGroup;

    private void Awake()
    {
        m_CanvasGroup = this.GetComponentInChildren<CanvasGroup>();

        //config.ResetBinds();
    }

    private void Start()
    {
        InitButtons();
    }

    public void ToggleWindow()
    {
        if(m_CanvasGroup.alpha == 1)
        {
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
            m_CanvasGroup.alpha = 0;

            foreach(EditKey key in buttons)
            {
                key.ResetText();
            }
        }
        else
        {
            m_CanvasGroup.interactable = true;
            m_CanvasGroup.blocksRaycasts = true;
            m_CanvasGroup.alpha = 1;
        }  
    }

    public void UpdateButton(int buttonToChange, KeyCode newKeyCode)
    {
        int result = config.UpdateButton(buttonToChange, newKeyCode);

        if(result != -1)
        {
            buttons[result].ResetText();
        }
    }

    private void InitButtons()
    {
        for(int i=0; i<buttons.Count; i++)
        {
            buttons[i].InitButton(config.buttonControls[i]);
        }
    }
}
