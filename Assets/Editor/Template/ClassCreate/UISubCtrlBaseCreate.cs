using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using ScriptsGenerate;

public class UISubCtrlBaseCreate : ClassBase
{
    public UISubCtrlBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }

        if (root.name.EndsWith(UIConstEditor.Str_UISubScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", UIConstEditor.Str_UISubScreenEndType);
            return;
        }

        AddNamespace(Const.Namespace_System_Collections);
        AddNamespace(Const.Namespace_System_Collections_Generic);
        AddNamespace(Const.Namespace_UnityEngine);
        AddNamespace(Const.Namespace_UnityEngine_UI);
        AddNamespace(Const.Namespace_TMPro);

        SetClassName(root.name + UIConstEditor.Str_UISubCtrlEndType);

        // 解析变量
        if (ParseField(root) == false)
        {
            return;
        }


        SetLegal(true);
    }

    #region 基类方法
    protected override string GetAccessModifier()
    {
        return Const.Access_Public;

    }

    protected override List<AbstractField> GetClassFields()
    {
        return FieldList;
    }

    protected override string GetClassName()
    {
        return ClassName;
    }

    protected override string GetParentClass()
    {
        return UIConstEditor.Class_UISubCtrlBase;
    }

    protected override List<string> GetUsingNamespace()
    {
        return NamespaceList;
    }
    #endregion
}
