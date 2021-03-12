using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ParameterBase : AbstractParameter
{
    private string m_ParamModifier = "";
    private string m_ParamName = "";
    private string m_ParamType = "";

    public ParameterBase(string modifier, string paramName, string paramType)
    {
        m_ParamModifier = modifier;
        m_ParamName = paramName;
        m_ParamType = paramType;
    }

    protected override string GetParameterModifier()
    {
        return m_ParamModifier;
    }

    protected override string GetParameterName()
    {
        return m_ParamName;
    }

    protected override string GetParameterType()
    {
        return m_ParamType;
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
