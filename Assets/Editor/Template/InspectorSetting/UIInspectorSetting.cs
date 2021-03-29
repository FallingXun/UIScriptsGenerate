using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class UIInspectorSetting : InspectorBase
{
    protected override List<Type> GetHandleType()
    {
        return new List<Type>()
        {
            typeof(UICtrlBase),
            typeof(UISubCtrlBase)
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
                object obj = UIScriptsHelper.GetObjectByTag(tfs[i].gameObject, tag);
                fieldsDic[name] = obj;
            }
        }
        return fieldsDic;
    }
}
