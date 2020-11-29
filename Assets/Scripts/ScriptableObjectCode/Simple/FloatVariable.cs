// Based on the architecture described by Ryan Hipple in his Unite 2017 talk
//
//https://www.youtube.com/watch?v=raQ3iHhE_Kk
//https://github.com/roboryantron/Unite2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName ="Variable/Float")]
public class FloatVariable : ScriptableObject
{
    public float Value;
    public float defaultValue;

    /// <summary>
    /// Changes the current Value by the given amount. Takes into consideration the max possible value.
    /// </summary>
    /// <param name="value">The value to modify the current Value.</param>
    public void ChangeValue(float value)
    {
        Value += value;
    }

    /// <summary>
    /// Changes the current Value by the given amount. Takes into consideration the max possible value.
    /// </summary>
    /// <param name="value">The value to modify the current Value.</param>
    public void ChangeValue(FloatVariable value)
    {
        Value += value.Value;
    }

    /// <summary>
    /// Sets the current Value to the given value. Does not take into consideration the max possible value.
    /// </summary>
    /// <param name="value">The value to set the current Value to.</param>
    public void SetValue(float value)
    {
        Value = value;
    }

    /// <summary>
    /// Sets the current Value to the given value. Does not take into consideration the max possible value.
    /// </summary>
    /// <param name="value">The value to set the current Value to.</param>
    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
    }

    /// <summary>
    /// Resets the current Value to the defaultValue.
    /// </summary>
    public void ResetValue()
    {
        Value = defaultValue;
    }
}
