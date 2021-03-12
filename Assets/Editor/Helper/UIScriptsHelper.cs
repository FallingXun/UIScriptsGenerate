using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;

public class UIScriptsHelper
{
    /// <summary>
    /// 获取标签类型
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static Type GetTagType(string tag)
    {
        Type t = null;
        if (Const.m_ComponentDict.TryGetValue(tag, out t))
        {
            return t;
        }
        return null;
    }

    /// <summary>
    /// 获取C#变量名
    /// </summary>
    /// <param name="baseName"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static string GetFieldName(string baseName, string tag)
    {
        return string.Format("m_{0}{1}{2}", baseName, Const.Symbol_Combine, tag);
    }

    /// <summary>
    /// 解析物体名返回标签信息
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static TagData ParseName(GameObject go)
    {
        TagData msg = new TagData();
        if (go == null)
        {
            msg.name = "";
            msg.tags = null;
        }
        else
        {
            string[] str = go.name.Split(new string[] { Const.Symbol_Split }, StringSplitOptions.RemoveEmptyEntries);
            if (str == null || str.Length <= 0)
            {
                msg.name = "";
                msg.tags = null;
            }
            else
            {
                msg.name = str[0];
                HashSet<string> strList = new HashSet<string>();
                for (int i = 1; i < str.Length; i++)
                {
                    strList.Add(str[i]);
                }
                msg.tags = strList;
            }
        }
        return msg;
    }

    /// <summary>
    /// 通过标签信息生成名字
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static string CombineName(TagData msg)
    {
        if (string.IsNullOrEmpty(msg.name))
        {
            return "";
        }
        if (msg.tags == null)
        {
            return msg.name;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append(msg.name);
        foreach (var tag in msg.tags)
        {
            builder.Append(Const.Symbol_Split);
            builder.Append(tag);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 查找C#脚本文件
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public static FileInfo FindClassFileInfo(string className)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/");
        foreach (FileInfo file in dir.GetFiles("*.cs", SearchOption.AllDirectories))
        {
            if (file.Name == className + ".cs")
            {
                return file;
            }
        }
        return null;
    }

}

public struct TagData
{
    public string name;
    public HashSet<string> tags;
}
