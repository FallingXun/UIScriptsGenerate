using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;
using System;

public class ClassBase : AbstractClass
{
    #region 变量定义

    private bool m_Legal = false;
    /// <summary>
    /// 是否合法，表明当前对象是否能正确使用
    /// </summary>
    public bool IsLegal
    {
        get
        {
            return m_Legal;
        }
    }

    private string m_ClassName = "";
    /// <summary>
    /// 类名
    /// </summary>
    public string ClassName
    {
        get
        {
            return m_ClassName;
        }
    }

    private string m_ClassStr = "";
    /// <summary>
    /// 类文本
    /// </summary>
    public string ClassStr
    {
        get
        {
            return m_ClassStr;
        }
    }

    private List<AbstractField> m_FieldList = new List<AbstractField>();
    /// <summary>
    /// 变量列表
    /// </summary>
    public List<AbstractField> FieldList
    {
        get
        {
            return m_FieldList;
        }
    }

    private List<AbstractMethod> m_MethodList = new List<AbstractMethod>();
    /// <summary>
    /// 方法列表
    /// </summary>
    public List<AbstractMethod> MethodList
    {
        get
        {
            return m_MethodList;
        }
    }

    private List<string> m_NamespaceList = new List<string>();
    /// <summary>
    /// 命名空间列表
    /// </summary>
    public List<string> NamespaceList
    {
        get
        {
            return m_NamespaceList;
        }
    }

    private AbstractMethod m_ConstructFunc;
    /// <summary>
    /// 构造函数
    /// </summary>
    public AbstractMethod ConstructFunc
    {
        get
        {
            return m_ConstructFunc;
        }
    }

    private string m_Annotation;
    /// <summary>
    /// 注释
    /// </summary>
    public string Annotation
    {
        get
        {
            return m_Annotation;
        }
    }

    #endregion

    #region 通用方法
    /// <summary>
    /// 设置合法性
    /// </summary>
    /// <param name="state"></param>
    protected void SetLegal(bool state)
    {
        m_Legal = state;
    }

    /// <summary>
    /// 设置类名
    /// </summary>
    /// <param name="name"></param>
    protected void SetClassName(string name)
    {
        m_ClassName = name;
    }

    /// <summary>
    /// 设置构造函数
    /// </summary>
    /// <param name="func"></param>
    protected void SetConstructFunc(AbstractMethod func)
    {
        m_ConstructFunc = func;
    }

    /// <summary>
    /// 设置类源文本
    /// </summary>
    /// <param name="str"></param>
    protected void SetClassText(string str)
    {
        m_ClassStr = str;
    }

    /// <summary>
    /// 添加命名空间
    /// </summary>
    /// <param name="ns"></param>
    protected void AddNamespace(string ns)
    {
        m_NamespaceList.Add(ns);
    }

    /// <summary>
    /// 添加变量
    /// </summary>
    /// <param name="field"></param>
    protected void AddField(AbstractField field)
    {
        m_FieldList.Add(field);
    }

    /// <summary>
    /// 添加方法
    /// </summary>
    /// <param name="method"></param>
    protected void AddMethod(AbstractMethod method)
    {
        m_MethodList.Add(method);
    }

    /// <summary>
    /// 添加注释
    /// </summary>
    /// <param name="annotation"></param>
    protected void SetAnnotation(string annotation)
    {
        m_Annotation = annotation;
    }

    /// <summary>
    /// 解析子节点C#变量
    /// </summary>
    /// <param name="root"></param>
    protected bool ParseField(GameObject root)
    {
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return false;
        }

        foreach (var tf in tfs)
        {
            if (UIScriptsHelper.IsIgnored(tf.gameObject))
            {
                continue;
            }
            TagData data = UIScriptsHelper.ParseName(tf.gameObject);
            if (data.tags == null || data.tags.Count <= 0)
            {
                continue;
            }
            foreach (var tag in data.tags)
            {
                Type type = UIScriptsHelper.GetObjectTypeByTag(tf.gameObject, tag);
                if (type == null)
                {
                    continue;
                }
                string fieldType = type.Name;
                string fieldName = UIScriptsHelper.GetFieldName(data.name, tag);
                FieldBase field = new FieldBase(Const.Access_Public, "", fieldName, fieldType, "");
                AddField(field);
            }
        }
        return true;
    }

    /// <summary>
    /// 更新子节点C#变量
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    protected bool UpdateField(GameObject root)
    {
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return false;
        }

        // 反射获取类信息
        Assembly assembly = typeof(UICtrlBase).Assembly;
        if (assembly == null)
        {
            return false;
        }
        Type classType = assembly.GetType(ClassName);
        if (classType == null)
        {
            Debug.LogErrorFormat("找不到类 {0} ，请重新生成！", ClassName);
            return false;
        }


        FileInfo file = UIScriptsHelper.FindClassFileInfo(ClassName);
        if (file == null)
        {
            Debug.LogErrorFormat("找不到类文件 {0}.cs ，请重新生成！", ClassName);
            return false;
        }
        using (var reader = file.OpenText())
        {
            SetClassText(reader.ReadToEnd());
        }
        if (string.IsNullOrEmpty(ClassStr))
        {
            Debug.LogErrorFormat("读取 {0} 类文件内容失败!", ClassName);
            return false;
        }

        foreach (var tf in tfs)
        {
            if (UIScriptsHelper.IsIgnored(tf.gameObject))
            {
                continue;
            }
            TagData data = UIScriptsHelper.ParseName(tf.gameObject);
            if (data.tags == null || data.tags.Count <= 0)
            {
                continue;
            }
            foreach (var tag in data.tags)
            {
                Type type = UIScriptsHelper.GetObjectTypeByTag(tf.gameObject, tag);
                if (type == null)
                {
                    continue;
                }
                string fieldType = type.Name;
                string fieldName = UIScriptsHelper.GetFieldName(data.name, tag);
                FieldInfo fieldInfo = classType.GetField(fieldName);
                if (fieldInfo != null)
                {
                    continue;
                }
                FieldBase field = new FieldBase(Const.Access_Public, "", fieldName, fieldType, "");
                AddField(field);
            }
        }

        return true;
    }
    #endregion

    #region 基类方法
    protected override string GetAccessModifier()
    {
        return null;
    }

    protected override List<AbstractField> GetClassFields()
    {
        return null;
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

    protected override string GetClassAnnotation()
    {
        return null;
    }

    public override string UpdateClass(string oldClass)
    {
        oldClass = oldClass.Replace(Const.Str_NormalSpace + Const.Sign_Fields, Const.Sign_Fields);
        oldClass = oldClass.Replace(Const.Str_NormalSpace + Const.Sign_Methods, Const.Sign_Methods);
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

        var annotation = GetClassAnnotation();
        if (string.IsNullOrEmpty(annotation) == false)
        {
            builder.AppendLine(annotation);
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
        builder.AppendLine(Const.Str_NormalSpace + Const.Sign_Fields);
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
        builder.AppendLine(Const.Str_NormalSpace + Const.Sign_Methods);
        return builder.ToString();
    }

    #endregion
}
