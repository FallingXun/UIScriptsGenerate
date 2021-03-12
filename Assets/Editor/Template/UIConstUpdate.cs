using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;

public class UIConstUpdate : ClassBase
{
    public UIConstUpdate(GameObject root)
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

        m_ClassName = typeof(UIConst).Name;

        // 反射获取类信息
        Assembly assembly = typeof(UIConst).Assembly;
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

        string fieldName = string.Format("UI{0}", root.name);
        string defaultValue = string.Format("\"{0}\"", root.name);
        FieldInfo fieldInfo = classType.GetField(fieldName);
        if (fieldInfo != null)
        {
            return;
        }
        FieldBase field = new FieldBase(Const.Access_Public, Const.Declaration_Const, fieldName, Const.Return_String, defaultValue);
        m_FieldList.Add(field);

        m_Legal = true;
    }


    #region 基类方法
    protected override List<AbstractField> GetClassFields()
    {
        return m_FieldList;
    }
    #endregion
}
