using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LuaClassBase : ClassBase
{
    public override string CreateClass()
    {
        StringBuilder builder = new StringBuilder();

        var annotation = GetClassAnnotation();
        if (string.IsNullOrEmpty(annotation) == false)
        {
            builder.AppendLine(annotation);
        }

        var className = GetClassName();
        if (string.IsNullOrEmpty(className))
        {
            return null;
        }

        // 方法
        var method_part = CombineMethod();
        builder.Append(method_part);
        builder.AppendLine();

        return builder.ToString();
    }

    public override string UpdateClass(string oldClass)
    {
        return null;
    }


    /// <summary>
    /// 组合方法部分
    /// </summary>
    /// <returns></returns>
    private string CombineMethod()
    {
        StringBuilder builder = new StringBuilder();
        var methods = GetClassMethods();
        if (methods != null)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                builder.Append(methods[i].GetValue());
                builder.AppendLine();
            }
        }
        // 添加后续插入标识
        builder.AppendLine(Const.Sign_Lua_Methods);
        return builder.ToString();
    }
}
