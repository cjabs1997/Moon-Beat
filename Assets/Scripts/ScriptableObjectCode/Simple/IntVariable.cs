using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "Variable/Int")]
public class IntVariable : NumVariable<int>
{
    /// <summary>
    /// Changes the current Value by the given amount.
    /// </summary>
    /// <param name="value">The value to modify the current Value.</param>
    public override void ChangeValue(int value)
    {
        Value += value;
    }

    /// <summary>
    /// Changes the current Value by the given amount.
    /// </summary>
    /// <param name="value">The value to modify the current Value.</param>
    public void ChangeValue(IntVariable value)
    {
        Value += value.Value;
    }

    /// <summary>
    /// Sets the current Value to the given value.
    /// </summary>
    /// <param name="value">The value to set the current Value to.</param>
    public override void SetValue(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Sets the current Value to the given value.
    /// </summary>
    /// <param name="value">The value to set the current Value to.</param>
    public void SetValue(IntVariable value)
    {
        Value = value.Value;
    }


    /// <summary>
    /// Resets the current value to its default values.
    /// </summary>
    public override void ResetValue()
    {
        Value = defaultValue;
    }
}
