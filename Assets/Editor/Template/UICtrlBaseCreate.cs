﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

public class UICtrlBaseCreate : ClassBase
{
    public UICtrlBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        if (root.name.EndsWith(Const.Str_UISubScreenEndType))
        {
            Debug.LogErrorFormat("命名以 {0} 结尾的物体请使用 {1} 生成！", Const.Str_UIScreenEndType, Const.Str_UISubScreenEndType);
            return;
        }
        if (root.name.EndsWith(Const.Str_UIScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", Const.Str_UIScreenEndType);
            return;
        }
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return;
        }
        m_ClassName = root.name + Const.Str_UICtrlEndType;
        foreach (var tf in tfs)
        {
            TagData data = UIScriptsHelper.ParseName(tf.gameObject);
            if (data.tags == null || data.tags.Count <= 0)
            {
                continue;
            }
            foreach (var tag in data.tags)
            {
                var t = UIScriptsHelper.GetTagType(tag);
                if (t == null)
                {
                    continue;
                }
                string fieldType = t.Name;
                string fieldName = UIScriptsHelper.GetFieldName(data.name, tag);
                FieldBase field = new FieldBase(Const.Access_Public, "", fieldName, fieldType, "");
                m_FieldList.Add(field);
            }
        }

        m_Legal = true;
    }

    #region 基类方法
    protected override string GetAccessModifier()
    {
        return Const.Access_Public;

    }

    protected override List<AbstractField> GetClassFields()
    {
        return m_FieldList;
    }

    protected override string GetClassName()
    {
        return m_ClassName;
    }

    protected override string GetParentClass()
    {
        return Const.Class_UICtrlBase;
    }

    protected override List<string> GetUsingNamespace()
    {
        List<string> l = new List<string>()
        {
            Const.Namespace_System_Collections,
            Const.Namespace_System_Collections_Generic,
            Const.Namespace_UnityEngine,
            Const.Namespace_UnityEngine_UI,
            Const.Namespace_TMPro
        };
        return l;
    }
    #endregion
}
