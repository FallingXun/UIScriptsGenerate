using System.Text;
using System.Collections.Generic;
using UnityEditor;

public class ClassBase : AbstractClass
{
    protected string m_ClassName = "";
    protected bool m_Legal = false;
    protected List<AbstractField> m_FieldList = new List<AbstractField>();
    protected List<AbstractMethod> m_MethodList = new List<AbstractMethod>();
    protected string m_ClassStr = "";

    public bool IsLegal
    {
        get
        {
            return m_Legal;
        }
    }

    public string ClassName
    {
        get
        {
            return m_ClassName;
        }
    }

    public string ClassStr
    {
        get
        {
            return m_ClassStr;
        }
    }


    #region 基类方法
    protected override string GetAccessModifier()
    {
        return null;
    }

    protected override List<AbstractField> GetClassFields()
    {
        return m_FieldList;
    }

    protected override List<AbstractMethod> GetClassMethods()
    {
        return null;
    }

    protected override string GetClassName()
    {
        return null;
    }

    protected override AbstractMethod GetConstructedFunction()
    {
        return null;
    }

    protected override string GetDeclarationModifier()
    {
        return null;
    }

    protected override string GetParentClass()
    {
        return null;
    }

    protected override List<string> GetUsingNamespace()
    {
        return null;
    }

    public override string UpdateClass(string oldClass)
    {
        return oldClass.Replace(Const.Sign_Fields, CombineFields()).Replace(Const.Sign_Methods, CombineMethod());
    }

    public override string CreateClass()
    {
        StringBuilder builder = new StringBuilder();

        string format = "{0} ";
        var usingNamespace = GetUsingNamespace();
        if (usingNamespace != null)
        {
            for (int i = 0; i < usingNamespace.Count; i++)
            {
                builder.AppendLine(string.Format("using {0};", usingNamespace[i]));
            }
            builder.AppendLine();
        }

        var access = GetAccessModifier();
        if (string.IsNullOrEmpty(access))
        {
            access = Const.Access_Public;
        }
        builder.AppendFormat(format, access);

        var delaration = GetDeclarationModifier();
        if (string.IsNullOrEmpty(delaration) == false)
        {
            builder.AppendFormat(format, delaration);
        }

        builder.AppendFormat(format, "class");

        builder.Append(GetClassName());

        var parentClass = GetParentClass();
        if (string.IsNullOrEmpty(parentClass) == false)
        {
            builder.AppendFormat(" : {0}", parentClass);
        }
        builder.AppendLine();
        builder.AppendLine("{");

        // 变量
        var field_part = CombineFields();
        builder.Append(field_part);
        builder.AppendLine();

        // 构造函数
        var construct_part = CombineConstruct();
        builder.Append(construct_part);
        builder.AppendLine();

        // 方法
        var method_part = CombineMethod();
        builder.Append(method_part);
        builder.AppendLine();

        builder.AppendLine("}");

        return builder.ToString();
    }

    /// <summary>
    /// 组合变量部分
    /// </summary>
    /// <returns></returns>
    private string CombineFields()
    {
        StringBuilder builder = new StringBuilder();
        var fields = GetClassFields();
        if (fields != null)
        {
            string fieldFormat = "    {0}";
            for (int i = 0; i < fields.Count; i++)
            {
                builder.AppendLine(string.Format(fieldFormat, fields[i].GetValue()));
            }
        }
        // 添加后续插入标识
        builder.AppendLine(Const.Sign_Fields);
        return builder.ToString();
    }

    /// <summary>
    /// 组合构造函数部分
    /// </summary>
    /// <returns></returns>
    private string CombineConstruct()
    {
        StringBuilder builder = new StringBuilder();
        var construct = GetConstructedFunction();
        if (construct != null)
        {
            builder.Append(construct.GetValue());
        }
        return builder.ToString();
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
        builder.AppendLine(Const.Sign_Methods);
        return builder.ToString();
    }

    #endregion
}
