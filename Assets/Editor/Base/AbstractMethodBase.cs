using System.Text;
using System.Collections.Generic;

public abstract class AbstractMethodBase
{
    /// <summary>
    /// 访问修饰符
    /// </summary>
    /// <returns></returns>
    public abstract string GetAccessModifier();

    /// <summary>
    /// 声明修饰符
    /// </summary>
    /// <returns></returns>
    public abstract string GetDeclarationModifier();

    /// <summary>
    /// 返回类型
    /// </summary>
    /// <returns></returns>
    public abstract string GetReturnType();

    /// <summary>
    /// 方法名
    /// </summary>
    /// <returns></returns>
    public abstract string GetMethodName();

    /// <summary>
    /// 泛型类型（未实现）
    /// </summary>
    /// <returns></returns>
    public abstract string GetGenericity();

    /// <summary>
    /// 参数列表
    /// </summary>
    /// <returns></returns>
    public abstract List<AbstractParameterBase> GetMethodParameters();

    /// <summary>
    /// 父类参数值列表
    /// </summary>
    /// <returns></returns>
    public abstract List<string> GetParentParameterValues();

    /// <summary>
    /// 泛型限定（未实现）
    /// </summary>
    /// <returns></returns>
    public abstract string GetGenericityLimited();

    public abstract List<string> GetMethodBody();

    protected virtual string GetPrefixSpace()
    {
        return "";
    }

    public override string ToString()
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
            returnType = "void";
        }
        builder.AppendFormat(format, returnType);

        builder.AppendFormat(format, GetMethodName());

        builder.Append("(");
        var parameters = GetMethodParameters();
        if (parameters != null)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                builder.Append(parameters[i].ToString());
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
