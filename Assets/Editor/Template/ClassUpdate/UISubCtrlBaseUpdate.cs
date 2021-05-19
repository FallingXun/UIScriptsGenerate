using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;
using ScriptsGenerate;

public class UISubCtrlBaseUpdate : ClassBase
{
    public UISubCtrlBaseUpdate(GameObject root)
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
     

        SetClassName(root.name + UIConstEditor.Str_UISubCtrlEndType);

        // 更新变量
        if (UpdateField<UISubCtrlBase>(root) == false)
        {
            return;
        }

        SetLegal(true);
    }

    #region 基类方法
    protected override List<AbstractField> GetClassFields()
    {
        return FieldList;
    }

    #endregion
}
