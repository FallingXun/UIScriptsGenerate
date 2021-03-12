﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;

public class UICtrlBaseUpdate : ClassBase
{
    public UICtrlBaseUpdate(GameObject root)
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

        // 反射获取类信息
        Assembly assembly = typeof(UICtrlBase).Assembly;
        if (assembly == null)
        {
            return;
        }
        Type classType = assembly.GetType(m_ClassName);
        if (classType == null)
        {
            Debug.LogErrorFormat("找不到类 {0} ，请重新生成！", m_ClassName);
            return;
        }


        FileInfo file = UIScriptsHelper.FindClassFileInfo(m_ClassName);
        if (file == null)
        {
            Debug.LogErrorFormat("找不到类文件 {0}.cs ，请重新生成！", m_ClassName);
            return;
        }
        using (var reader = file.OpenText())
        {
            m_ClassStr = reader.ReadToEnd();
        }
        if (string.IsNullOrEmpty(m_ClassStr))
        {
            Debug.LogErrorFormat("读取 {0} 类文件内容失败!", m_ClassName);
            return;
        }
 
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
                FieldInfo fieldInfo = classType.GetField(fieldName);
                if (fieldInfo != null)
                {
                    continue;
                }
                FieldBase field = new FieldBase(Const.Access_Public, "", fieldName, fieldType, "");
                m_FieldList.Add(field);
            }
        }

        m_Legal = true;
    }

    #region 基类方法
    protected override List<AbstractField> GetClassFields()
    {
        return m_FieldList;
    }

    #endregion
}
