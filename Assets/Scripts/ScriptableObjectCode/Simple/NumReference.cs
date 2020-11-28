using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumReference<T>
{
    public T constantValue;
    public bool useConstant;

    public NumReference()
    { }

    public NumReference(T value)
    {
        useConstant = true;
        constantValue = value;
    }

    public abstract void SetValue(T val);
    public abstract void ChangeValue(T val);
}
