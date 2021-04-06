using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;

public class UIStyleChangeEditor : Editor
{
    private const string UIFieldDataPath = "Assets/UIFieldData/";

    private static Dictionary<EUIBindItemType, string> m_BindTypeDic = new Dictionary<EUIBindItemType, string>()
    {
        {EUIBindItemType.GameObject,Const.Tag_GameObject },
        {EUIBindItemType.Image,Const.Tag_Image },
        {EUIBindItemType.Text,Const.Tag_TextMeshProUGUI },
        {EUIBindItemType.Button,Const.Tag_Button },
        {EUIBindItemType.Toggle,Const.Tag_Toggle },
        {EUIBindItemType.Slider,Const.Tag_Slider },
        {EUIBindItemType.DropDown,Const.Tag_DropDown },
        {EUIBindItemType.Scroll,Const.Tag_ScrollRect },
        {EUIBindItemType.Input,Const.Tag_InputField },
        {EUIBindItemType.RectTransform,Const.Tag_RectTransform },
        {EUIBindItemType.ReusableLayoutGroup,Const.Tag_ReusableLayoutGroup },
        {EUIBindItemType.CommonItemScrollView,Const.Tag_Item },
    };

    [MenuItem("UITools/修改预制体名")]
    public static void ChangePrefabName()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            return;
        }
        Dictionary<string, string> nameDic = null;
        var cb = go.GetComponent<UICtrlBase>();
        if (cb != null)
        {
            var lcb = cb as LuaCtrlBase;
            if (lcb != null)
            {
                nameDic = ChangeLuaPrefab(lcb.m_LuaBindItems);
            }
            else
            {
                var fields = cb.GetType().GetFields();
                nameDic = ChangeCSharpPrefab(fields, cb);
            }
        }
        else
        {
            var scb = go.GetComponent<UISubCtrlBase>();
            if (scb != null)
            {
                var lscb = scb as LuaSubCtrlBase;
                if (lscb != null)
                {
                    nameDic = ChangeLuaPrefab(lscb.m_LuaBindItems);
                }
                else
                {
                    var fields = scb.GetType().GetFields();
                    nameDic = ChangeCSharpPrefab(fields, scb);
                }
            }
        }
        GenerateComparedData(nameDic, go.name);
    }

    private static Dictionary<string, string> ChangeCSharpPrefab(FieldInfo[] infos, object obj)
    {
        if (infos == null)
        {
            return null;
        }
        Dictionary<string, string> nameDic = new Dictionary<string, string>();
        foreach (var item in infos)
        {
            var val = item.GetValue(obj);
            GameObject go = null;
            if (val is Component)
            {
                var com = val as Component;
                go = com.gameObject;
            }
            else if (val is MonoBehaviour)
            {
                var mono = val as MonoBehaviour;
                go = mono.gameObject;
            }
            else if (val is GameObject)
            {
                go = val as GameObject;
            }
            if (go != null)
            {
                var prefabStage = PrefabStageUtility.GetPrefabStage(go);
                if (prefabStage != null)
                {
                    if (UIScriptsHelper.IsIgnored(go))
                    {
                        continue;
                    }
                    var type = item.FieldType;
                    foreach (var t in Const.m_ComponentDict)
                    {
                        if (t.Value == type)
                        {
                            var data = UIScriptsHelper.ParseName(go);
                            string tag = t.Key;
                            data.tags.Add(tag);
                            Selection.activeGameObject = go;
                            HierarchyEditor.AddTag(tag);
                            nameDic[item.Name] = UIScriptsHelper.GetFieldName(data.name, tag);
                            break;
                        }
                    }
                }
            }
        }
        return nameDic;
    }

    private static Dictionary<string, string> ChangeLuaPrefab(List<LuaBindItem> bindItems)
    {
        if (bindItems == null)
        {
            return null;
        }
        Dictionary<string, string> nameDic = new Dictionary<string, string>();
        foreach (var item in bindItems)
        {
            if (UIScriptsHelper.IsIgnored(item.go))
            {
                continue;
            }
            string tag = "";
            if (m_BindTypeDic.TryGetValue(item.bindType, out tag))
            {
                var data = UIScriptsHelper.ParseName(item.go);
                data.tags.Add(tag);
                Selection.activeGameObject = item.go;
                HierarchyEditor.AddTag(tag);
                nameDic[item.variableName] = UIScriptsHelper.GetFieldName(data.name, tag);
            }
        }
        return nameDic;
    }

    private static void GenerateComparedData(Dictionary<string, string> data, string name)
    {
        if (data == null)
        {
            return;
        }

        string directory = Path.Combine(Directory.GetCurrentDirectory(), UIFieldDataPath);
        if (Directory.Exists(directory) == false)
        {
            Directory.CreateDirectory(directory);
        }
        string path = directory + name + ".txt";
        StreamWriter writer = new StreamWriter(path, false);
        foreach (var item in data)
        {
            writer.WriteLine(string.Format("{0}:{1}", item.Key, item.Value));
        }
        writer.Close();
        writer.Dispose();
        AssetDatabase.Refresh();
    }


    [MenuItem("UITools/修改脚本变量名")]
    private static void AutoReplace()
    {
        string path = EditorUtility.OpenFilePanel("请选择要替换的脚本", Path.Combine(Directory.GetCurrentDirectory(), HierarchyEditor.UIScriptCreatePath), "");
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        string fieldPath = EditorUtility.OpenFilePanel("请选择脚本变量参考文件", Path.Combine(Directory.GetCurrentDirectory(), UIFieldDataPath), "");
        if (string.IsNullOrEmpty(fieldPath))
        {
            return;
        }
        Dictionary<string, string> nameDic = new Dictionary<string, string>();
        StreamReader reader = new StreamReader(fieldPath);
        string line = reader.ReadLine();
        while (line != null)
        {
            string[] str = line.Split(':');
            if (str != null && str.Length == 2)
            {
                nameDic[str[0]] = str[1];
            }
            line = reader.ReadLine();
        }
        reader.Close();
        reader.Dispose();

        string text = File.ReadAllText(path);
        foreach (var item in nameDic)
        {
            // 全字匹配
            string pattern = string.Format("\\b{0}\\b", item.Key);
            string replace = item.Value;
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, replace);
        }
        File.WriteAllText(path, text);

        AssetDatabase.Refresh();
    }
}
