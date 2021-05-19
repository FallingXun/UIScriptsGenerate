using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using ScriptsGenerate;

public class LuaUIInspectorSetting : InspectorBase
{
    private const string FieldName_BindItems = "m_LuaBindItems";
    private const string FieldName_TextAsset = "m_LuaFile";
    private const string LuaFilePath = "Assets/Scripts/Surface/UI/";

    protected override List<Type> GetHandleType()
    {
        return new List<Type>()
        {
            typeof(LuaCtrlBase),
            typeof(LuaSubCtrlBase)
        };
    }

    protected override Dictionary<string, object> ParseFieldsData(GameObject go)
    {
        Transform[] tfs = go.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return null;
        }

        TextAsset luaFile = null;
        List<LuaBindItem> bindItems = null;
        LuaCtrlBase ctrl = go.GetComponent<LuaCtrlBase>();
        if (ctrl != null)
        {
            luaFile = ctrl.m_LuaFile;
            bindItems = ctrl.m_LuaBindItems;
        }
        else
        {
            LuaSubCtrlBase subCtrl = go.GetComponent<LuaSubCtrlBase>();
            if (subCtrl != null)
            {
                luaFile = subCtrl.m_LuaFile;
                bindItems = subCtrl.m_LuaBindItems;
            }
            else
            {
                bindItems = new List<LuaBindItem>();
            }
        }

        // 解析预制体变量
        Dictionary<string, object> fieldsDic = new Dictionary<string, object>();
        Dictionary<string, LuaBindItem> itemDic = new Dictionary<string, LuaBindItem>();

        for (int i = 0; i < tfs.Length; i++)
        {
            if (ScriptsHelper.IsIgnored(tfs[i].gameObject))
            {
                continue;
            }
            TagData data = ScriptsHelper.ParseName(tfs[i].gameObject);
            if (data.tags == null || data.tags.Count <= 0)
            {
                continue;
            }
            foreach (var tag in data.tags)
            {
                string name = ScriptsHelper.GetFieldName(data.name, tag);
                //Type type = ScriptsHelper.GetObjectTypeByTag(tfs[i].gameObject, tag);
                LuaBindItem item = new LuaBindItem();
                item.variableName = name;
                item.bindTypeTag = tag;
                item.go = tfs[i].gameObject;
                itemDic[name] = item;
            }
        }
        foreach (var item in bindItems)
        {
            if (itemDic.ContainsKey(item.variableName))
            {
                var bind = itemDic[item.variableName];
                if (bind.go.Equals(item.go) && bind.bindTypeTag.Equals(item.bindTypeTag))
                {
                    continue;
                }
                Debug.LogError("lua绑定的变量出现同名，请检查：" + item.variableName);
            }
            else
            {
                itemDic[item.variableName] = item;
            }
        }
        bindItems.Clear();
        foreach (var item in itemDic.Values)
        {
            bindItems.Add(item);
        }
        fieldsDic[FieldName_BindItems] = bindItems;


        string directory = Path.Combine(Directory.GetCurrentDirectory(), LuaFilePath);
        if (Directory.Exists(directory) == false)
        {
            Debug.LogError("lua脚本路径不存在，请检查：" + directory);
        }
        else
        {
            string[] files = Directory.GetFiles(directory, go.name + ".lua", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                string path = files[0].Replace("\\", "/");
                int index = path.IndexOf(LuaFilePath);
                path = path.Substring(index, path.Length - index);
                luaFile = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            }
            if (luaFile != null)
            {
                fieldsDic[FieldName_TextAsset] = luaFile;
            }
        }

        return fieldsDic;
    }
}
