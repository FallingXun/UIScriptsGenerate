using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

public class UISubCtrlBaseCreate : ClassBase
{
    public UISubCtrlBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }

        if (root.name.EndsWith(Const.Str_UISubScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", Const.Str_UISubScreenEndType);
            return;
        }

        AddNamespace(Const.Namespace_System_Collections);
        AddNamespace(Const.Namespace_System_Collections_Generic);
        AddNamespace(Const.Namespace_UnityEngine);
        AddNamespace(Const.Namespace_UnityEngine_UI);
        AddNamespace(Const.Namespace_TMPro);

        SetClassName(root.name + Const.Str_UISubCtrlEndType);

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
        return Const.Class_UISubCtrlBase;
    }

    protected override List<string> GetUsingNamespace()
    {
        return NamespaceList;
    }
    #endregion
}
