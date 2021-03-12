using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ParameterBase : AbstractParameter
{
    protected override string GetParameterModifier()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetParameterName()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetParameterType()
    {
        throw new System.NotImplementedException();
    }

    public override string GetValue()
    {
        StringBuilder builder = new StringBuilder();

        var modifier = GetParameterModifier();
        if (string.IsNullOrEmpty(modifier) == false)
        {
            builder.Append(modifier);
            builder.Append(" ");
        }

        builder.Append(GetParameterType());
        builder.Append(" ");

        builder.Append(GetParameterName());

        return builder.ToString();
    }
}
