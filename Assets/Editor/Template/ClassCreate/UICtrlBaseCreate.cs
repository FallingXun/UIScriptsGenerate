using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using ScriptsGenerate;

public class UICtrlBaseCreate : ClassBase
{
    public UICtrlBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        if (root.name.EndsWith(UIConstEditor.Str_UISubScreenEndType))
        {
            Debug.LogErrorFormat("命名以 {0} 结尾的物体请使用 {1} 生成！", UIConstEditor.Str_UIScreenEndType, UIConstEditor.Str_UISubScreenEndType);
            return;
        }
        if (root.name.EndsWith(UIConstEditor.Str_UIScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", UIConstEditor.Str_UIScreenEndType);
            return;
        }

        AddNamespace(Const.Namespace_System_Collections);
        AddNamespace(Const.Namespace_System_Collections_Generic);
        AddNamespace(Const.Namespace_UnityEngine);
        AddNamespace(Const.Namespace_UnityEngine_UI);
        AddNamespace(Const.Namespace_TMPro);

        SetClassName(root.name + UIConstEditor.Str_UICtrlEndType);

        // 解析变量
        if(ParseField(root) == false)
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
        return UIConstEditor.Class_UICtrlBase;
    }

    protected override List<string> GetUsingNamespace()
    {
        return NamespaceList;
    }
    #endregion
}
