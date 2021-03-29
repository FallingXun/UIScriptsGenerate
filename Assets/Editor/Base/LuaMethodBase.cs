using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LuaMethodBase : MethodBase
{
    public override string GetValue()
    {
        StringBuilder builder = new StringBuilder();
        string format = "{0} ";

        if (string.IsNullOrEmpty(GetAnnotation()) == false)
        {
            builder.AppendLine(string.Format("-- {0}", GetAnnotation()));
        }

        var methodName = GetMethodName();
        // 构造函数没有变量名
        if (string.IsNullOrEmpty(methodName) == false)
        {
            builder.AppendFormat("function {0}", methodName);
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

        builder.AppendLine();

        var methodBody = GetMethodBody();
        if (methodBody != null && methodBody.Count > 0)
        {
            for (int i = 0; i < methodBody.Count; i++)
            {
                builder.AppendLine(methodBody[i]);
            }
        }
        else
        {
            builder.AppendLine();
        }

        builder.AppendLine("end");

        return builder.ToString();
    }
}
