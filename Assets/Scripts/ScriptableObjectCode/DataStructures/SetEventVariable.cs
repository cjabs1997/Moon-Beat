using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Similar to a generic SetVariable except this one will raise an event when emptied.
/// </summary>
/// <typeparam name="T">The type of object that will be contained within the set.</typeparam>
public class SetEventVariable<T> : ScriptableObject
{
    public List<T> Value = new List<T>();
    public GameEvent emptyEvent;


    public void AddValue(T val)
    {
        if (!Value.Contains(val))
        {
            Value.Add(val);
        }
    }

    public void RemoveValue(T val)
    {
        if (Value.Contains(val))
        {
            Value.Remove(val);

            if (Value.Count == 0)
            {
                emptyEvent.Raise();
            }
        }
    }

    public void ResetSet()
    {
        Value.Clear();
    }
}