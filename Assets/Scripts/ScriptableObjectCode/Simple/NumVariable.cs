using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumVariable<T> : ScriptableObject
{
    public T Value;
    public T defaultValue;

    public abstract void ResetValue();

    public abstract void SetValue(T val);
    public abstract void ChangeValue(T val);
}
