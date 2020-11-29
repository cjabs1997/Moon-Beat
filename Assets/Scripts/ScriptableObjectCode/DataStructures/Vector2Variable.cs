using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2", menuName = "Variable/Vector2")]
public class Vector2Variable : ScriptableObject
{
    public Vector2 Value;

    public void SetValue(Vector2 value)
    {
        Value = value;
    }

    public void SetValue(Vector2Variable value)
    {
        Value = value.Value;
    }

    // Can add more functionality if we need to.
}
