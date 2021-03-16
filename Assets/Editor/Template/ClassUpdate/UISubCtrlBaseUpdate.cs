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
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return;
        }

        SetClassName(root.name + Const.Str_UISubCtrlEndType);

        // 反射获取类信息
        Assembly assembly = typeof(UISubCtrlBase).Assembly;
        if (assembly == null)
        {
            return;
        }
        Type classType = assembly.GetType(ClassName);
        if (classType == null)
        {
            Debug.LogErrorFormat("找不到类 {0} ，请重新生成！", ClassName);
            return;
        }


        FileInfo file = UIScriptsHelper.FindClassFileInfo(ClassName);
        if (file == null)
        {
            Debug.LogErrorFormat("找不到类文件 {0}.cs ，请重新生成！", ClassName);
            return;
        }
        using (var reader = file.OpenText())
        {
            SetClassText(reader.ReadToEnd());
        }
        if (string.IsNullOrEmpty(ClassStr))
        {
            Debug.LogErrorFormat("读取 {0} 类文件内容失败!", ClassName);
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
                AddField(field);
            }
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
