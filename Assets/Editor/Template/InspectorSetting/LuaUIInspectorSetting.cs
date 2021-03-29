using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;

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

        // 解析预制体变量
        Dictionary<string, object> fieldsDic = new Dictionary<string, object>();
        List<LuaBindItem> bind = new List<LuaBindItem>();
        for (int i = 0; i < tfs.Length; i++)
        {
            if (PrefabUtility.IsPartOfAnyPrefab(tfs[i]) /*&& PrefabUtility.IsAnyPrefabInstanceRoot(tfs[i].gameObject) == false*/)
            {
                continue;
            }
            TagData data = UIScriptsHelper.ParseName(tfs[i].gameObject);
            if (data.tags == null || data.tags.Count <= 0)
            {
                continue;
            }
            foreach (var tag in data.tags)
            {
                string name = UIScriptsHelper.GetFieldName(data.name, tag);
                Type type = UIScriptsHelper.GetTagType(tag);
                LuaBindItem item = new LuaBindItem();
                item.variableName = name;
                item.bindType = type;
                item.go = tfs[i].gameObject;
                bind.Add(item);
            }
        }
        fieldsDic[FieldName_BindItems] = bind;

        TextAsset luaFile = null;
        LuaCtrlBase ctrl = go.GetComponent<LuaCtrlBase>();
        if (ctrl != null)
        {
            luaFile = ctrl.m_LuaFile;
        }
        else
        {
            LuaSubCtrlBase subCtrl = go.GetComponent<LuaSubCtrlBase>();
            if (subCtrl != null)
            {
                luaFile = subCtrl.m_LuaFile;
            }
        }

        string fileName = string.Format("name:{0}", go.name);
        string[] guids = AssetDatabase.FindAssets(fileName, new string[] { LuaFilePath });
        if (guids != null)
        {
            for (int i = 0; i < guids.Length; i++)
            {
                string p = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (p.EndsWith(fileName))
                {
                    luaFile = AssetDatabase.LoadAssetAtPath(p, typeof(TextAsset)) as TextAsset;
                    break;
                }
            }
        }
        fieldsDic[FieldName_TextAsset] = luaFile;

        return fieldsDic;
    }
}
