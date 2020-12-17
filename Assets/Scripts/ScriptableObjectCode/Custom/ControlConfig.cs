using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ControlConfig : ScriptableObject
{
    public List<KeyCode> buttonControls;
    public List<KeyCode> defaults;

    public int UpdateButton(int buttonToChange, KeyCode newKeyCode)
    {
        int index = -1;

        if(buttonControls.Contains(newKeyCode))
        {
            index = buttonControls.IndexOf(newKeyCode);

            buttonControls[index] = buttonControls[buttonToChange];
        }

        buttonControls[buttonToChange] = newKeyCode;

        return index;
    }

    public void ResetBinds()
    {
        for(int i = 0; i < defaults.Count; ++i)
        {
            buttonControls[i] = defaults[i];
        }
    }
}
