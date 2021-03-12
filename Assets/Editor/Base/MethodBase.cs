using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MethodBase : AbstractMethod
{
    protected override string GetAccessModifier()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetDeclarationModifier()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetGenericity()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetGenericityLimited()
    {
        throw new System.NotImplementedException();
    }

    protected override List<string> GetMethodBody()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetMethodName()
    {
        throw new System.NotImplementedException();
    }

    protected override List<AbstractParameter> GetMethodParameters()
    {
        throw new System.NotImplementedException();
    }

    protected override List<string> GetParentParameterValues()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetReturnType()
    {
        throw new System.NotImplementedException();
    }

    public override string GetValue()
    {
        StringBuilder builder = new StringBuilder();
        string format = "{0} ";

        builder.Append(GetPrefixSpace());

        var access = GetAccessModifier();
        if (string.IsNullOrEmpty(access) == false)
        {
            builder.AppendFormat(format, access);
        }

        var delaration = GetDeclarationModifier();
        if (string.IsNullOrEmpty(delaration) == false)
        {
            builder.AppendFormat(format, delaration);
        }

        var returnType = GetReturnType();
        if (string.IsNullOrEmpty(returnType))
        {
            returnType = Const.Return_Void;
        }
        builder.AppendFormat(format, returnType);

        builder.AppendFormat(format, GetMethodName());

        builder.Append("(");
        var parameters = GetMethodParameters();
        if (parameters != null)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                builder.Append(parameters[i].GetValue());
                if (i < parameters.Count - 1)
                {
                    builder.AppendFormat(format, ",");
                }
            }
        }
        builder.Append(")");

        var parentValues = GetParentParameterValues();
        if (parentValues != null)
        {
            builder.Append(" : base");
            builder.Append("(");
            for (int i = 0; i < parentValues.Count; i++)
            {
                builder.Append(parentValues[i]);
                if (i < parentValues.Count - 1)
                {
                    builder.AppendFormat(format, ",");
                }
            }
            builder.Append(")");
        }

        builder.AppendLine(GetPrefixSpace() + "{");
        builder.AppendLine(GetPrefixSpace());
        builder.AppendLine(GetPrefixSpace() + "}");

        return builder.ToString();
    }
}
