// Based on the architecture described by Ryan Hipple in his Unite 2017 talk
//
//https://www.youtube.com/watch?v=raQ3iHhE_Kk
//https://github.com/roboryantron/Unite2017

using System;

[Serializable]
public class FloatReference : NumReference<float>
{
    public FloatVariable variable;

    public FloatReference()
    { }

    public FloatReference(float value)
    {
        useConstant = true;
        constantValue = value;
    }
    
    public float Value
    {
        get { return useConstant ? constantValue : variable.Value; }
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }

    public override void ChangeValue(float amount)
    {
        if (useConstant)
            constantValue += amount;
        else
            variable.ChangeValue(amount);
    }

    public void ChangeValue(FloatVariable amount)
    {
        if (useConstant)
            constantValue += amount.Value;
        else
            variable.ChangeValue(amount);
    }

    public override void SetValue(float amount)
    {
        if (useConstant)
            constantValue = amount;
        else
            variable.Value = amount;
    }

    public void SetValue(FloatVariable amount)
    {
        if (useConstant)
            constantValue = amount.Value;
        else
            variable.Value = amount.Value;
    }
}
