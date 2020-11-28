using System;

[Serializable]
public class IntReference : NumReference<int>
{
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        useConstant = true;
        constantValue = value;
    }

    public int Value
    {
        get { return useConstant ? constantValue : Variable.Value; }
    }

    public static implicit operator int(IntReference reference)
    {
        return reference.Value;
    }

    public override void ChangeValue(int amount)
    {
        if (useConstant)
            constantValue += amount;
        else
            Variable.ChangeValue(amount);
    }

    public void ChangeValue(IntVariable amount)
    {
        if (useConstant)
            constantValue += amount.Value;
        else
            Variable.ChangeValue(amount);
    }

    public override void SetValue(int amount)
    {
        if (useConstant)
            constantValue = amount;
        else
            Variable.Value = amount;
    }

    public void SetValue(IntVariable amount)
    {
        if (useConstant)
            constantValue = amount.Value;
        else
            Variable.Value = amount.Value;
    }
}
