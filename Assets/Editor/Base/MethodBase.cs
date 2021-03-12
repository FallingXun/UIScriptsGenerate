using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MethodBase : AbstractMethod
{
    private string m_MethodName = "";
    private string m_ReturnType = "";
    private string m_AccessModifier = "";
    private string m_DeclarationModifier = "";
    private string m_Genericity = "";
    private string m_GenericityLimited = "";
    private List<AbstractParameter> m_ParamList;
    private List<string> m_ParentParamValueList;
    private List<string> m_MethodBody;

    public MethodBase(string access, string declaration, string methodName, string returnType)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
    }

    public MethodBase(string access, string declaration, string methodName, string returnType, List<AbstractParameter> paramList)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
        m_ParamList = paramList;
    }


    public MethodBase(string access, string declaration, string methodName, string returnType, List<string> parentParamValueList)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
        m_ParentParamValueList = parentParamValueList;
    }

    public MethodBase(string access, string declaration, string methodName, string returnType, List<AbstractParameter> paramList, List<string> parentParamValueList)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
        m_ParamList = paramList;
        m_ParentParamValueList = parentParamValueList;
    }

    public MethodBase(string access, string declaration, string methodName, string returnType, List<AbstractParameter> paramList, List<string> parentParamValueList, List<string> methodBody)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
        m_ParamList = paramList;
        m_ParentParamValueList = parentParamValueList;
        m_MethodBody = methodBody;
    }

    #region 基类方法
    protected override string GetAccessModifier()
    {
        return m_AccessModifier;
    }

    protected override string GetDeclarationModifier()
    {
        return m_DeclarationModifier;
    }

    protected override string GetGenericity()
    {
        return m_Genericity;
    }

    protected override string GetGenericityLimited()
    {
        return m_GenericityLimited;
    }

    protected override List<string> GetMethodBody()
    {
        return m_MethodBody;
    }

    protected override string GetMethodName()
    {
        return m_MethodName;
    }

    protected override List<AbstractParameter> GetMethodParameters()
    {
        return m_ParamList;
    }

    protected override List<string> GetParentParameterValues()
    {
        return m_ParentParamValueList;
    }

    protected override string GetReturnType()
    {
        return m_ReturnType;
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
        builder.Append(returnType);

        var methodName = GetMethodName();
        // 构造函数没有变量名
        if (string.IsNullOrEmpty(methodName) == false)
        {
            builder.AppendFormat(" {0}", methodName);
        }

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
        builder.AppendLine();
        builder.AppendLine(GetPrefixSpace() + "{");

        if (m_MethodBody != null && m_MethodBody.Count > 0)
        {
            for (int i = 0; i < m_MethodBody.Count; i++)
            {
                builder.AppendLine(GetPrefixSpace() + m_MethodBody[i]);
            }
        }
        else
        {
            builder.AppendLine(GetPrefixSpace());
        }

        builder.AppendLine(GetPrefixSpace() + "}");

        return builder.ToString();
    }
    #endregion
}
