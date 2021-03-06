using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;
using ScriptsGenerate;

public class UICtrlBaseUpdate : ClassBase
{
    public UICtrlBaseUpdate(GameObject root)
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

        SetClassName(root.name + UIConstEditor.Str_UICtrlEndType);

        // 更新变量
        if(UpdateField<UICtrlBase>(root) == false)
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
