using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;

public class UISubCtrlBaseUpdate : ClassBase
{
    public UISubCtrlBaseUpdate(GameObject root)
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
     

        SetClassName(root.name + Const.Str_UISubCtrlEndType);

        // 更新变量
        if (UpdateField(root) == false)
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
