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
    private string m_Annotation = "";
    private List<AbstractParameter> m_ParamList;
    private List<string> m_ParentParamValueList;
    private List<string> m_MethodBody;

    public MethodBase()
    {

    }

    /// <summary>
    /// 方法
    /// </summary>
    /// <param name="access">访问修饰符</param>
    /// <param name="declaration">声明修饰符</param>
    /// <param name="methodName">方法名</param>
    /// <param name="returnType">返回类型</param>
    /// <param name="annotation">注释文本</param>
    /// <param name="paramList">参数列表</param>
    /// <param name="parentParamValueList">父类参数值</param>
    /// <param name="methodBody">方法内容</param>
    public MethodBase(string access, string declaration, string methodName, string returnType, string annotation, List<AbstractParameter> paramList, List<string> parentParamValueList, List<string> methodBody)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_MethodName = methodName;
        m_ReturnType = returnType;
        m_ParamList = paramList;
        m_ParentParamValueList = parentParamValueList;
        m_MethodBody = methodBody;
        m_Annotation = annotation;
    }

    /// <summary>
    /// 访问修饰符
    /// </summary>
    /// <param name="access"></param>
    /// <returns></returns>
    public MethodBase SetAccess(string access)
    {
        m_AccessModifier = access;
        return this;
    }

    /// <summary>
    /// 声明修饰符
    /// </summary>
    /// <param name="declaration"></param>
    /// <returns></returns>
    public MethodBase SetDeclaration(string declaration)
    {
        m_DeclarationModifier = declaration;
        return this;
    }

    /// <summary>
    /// 方法名
    /// </summary>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public MethodBase SetMethodName(string methodName)
    {
        m_MethodName = methodName;
        return this;
    }

    /// <summary>
    /// 返回类型
    /// </summary>
    /// <param name="returnType"></param>
    /// <returns></returns>
    public MethodBase SetReturnType(string returnType)
    {
        m_ReturnType = returnType;
        return this;
    }

    /// <summary>
    /// 注释文本
    /// </summary>
    /// <param name="annotation"></param>
    /// <returns></returns>
    public MethodBase SetAnnotation(string annotation)
    {
        m_Annotation = annotation;
        return this;
    }

    /// <summary>
    /// 参数列表
    /// </summary>
    /// <param name="paramList"></param>
    /// <returns></returns>
    public MethodBase SetParamList(List<AbstractParameter> paramList)
    {
        m_ParamList = paramList;
        return this;
    }

    /// <summary>
    /// 父类参数值
    /// </summary>
    /// <param name="parentParamValueList"></param>
    /// <returns></returns>
    public MethodBase SetParentParamValueList(List<string> parentParamValueList)
    {
        m_ParentParamValueList = parentParamValueList;
        return this;
    }

    /// <summary>
    /// 方法内容
    /// </summary>
    /// <param name="methodBody"></param>
    /// <returns></returns>
    public MethodBase SetMethodBody(List<string> methodBody)
    {
        m_MethodBody = methodBody;
        return this;
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

    protected override string GetAnnotation()
    {
        return m_Annotation;
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

        if (string.IsNullOrEmpty(GetAnnotation()) == false)
        {
            builder.Append(GetPrefixSpace());
            builder.AppendLine(string.Format("// {0}", GetAnnotation()));
        }

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

        var methodBody = GetMethodBody();
        if (methodBody != null && methodBody.Count > 0)
        {
            for (int i = 0; i < methodBody.Count; i++)
            {
                builder.AppendLine(GetPrefixSpace() + methodBody[i]);
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
