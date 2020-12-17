using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitButtons : MonoBehaviour
{
    public ControlConfig controls;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ButtonController[] buttons = this.GetComponentsInChildren<ButtonController>();

        for(int i = 0; i < controls.buttonControls.Count; ++i)
        {
            buttons[i].activationKey = controls.buttonControls[i];
        }

    }
}
