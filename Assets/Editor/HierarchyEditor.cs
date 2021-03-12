﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;

public class HierarchyEditor : Editor
{
    private const string UIScriptCreatePath = "/Script/Surface/UI/";

    #region 工具

    #region GameObject
    [MenuItem("GameObject/UI标签/GameObject/添加标签", false, 0)]
    public static void AddTag_GameObject()
    {
        AddTag(Const.Tag_GameObject);
    }

    [MenuItem("GameObject/UI标签/GameObject/移除标签", false, 0)]
    public static void RemoveTag_GameObject()
    {
        RemoveTag(Const.Tag_GameObject);
    }
    #endregion

    #region Transform
    [MenuItem("GameObject/UI标签/Transform/添加标签", false, 0)]
    public static void AddTag_Transform()
    {
        AddTag(Const.Tag_Transform);
    }

    [MenuItem("GameObject/UI标签/Transform/移除标签", false, 0)]
    public static void RemoveTag_Transform()
    {
        RemoveTag(Const.Tag_Transform);
    }
    #endregion

    #region RectTransform
    [MenuItem("GameObject/UI标签/RectTransform/添加标签", false, 0)]
    public static void AddTag_RectTransform()
    {
        AddTag(Const.Tag_RectTransform);
    }

    [MenuItem("GameObject/UI标签/RectTransform/移除标签", false, 0)]
    public static void RemoveTag_RectTransform()
    {
        RemoveTag(Const.Tag_RectTransform);
    }
    #endregion

    #region TextMeshProUGUI
    [MenuItem("GameObject/UI标签/TextMeshProUGUI/添加标签", false, 0)]
    public static void AddTag_TextMeshProUGUI()
    {
        AddTag(Const.Tag_TextMeshProUGUI);
    }

    [MenuItem("GameObject/UI标签/TextMeshProUGUI/移除标签", false, 0)]
    public static void RemoveTag_TextMeshProUGUI()
    {
        RemoveTag(Const.Tag_TextMeshProUGUI);
    }
    #endregion

    #region Image
    [MenuItem("GameObject/UI标签/Image/添加标签", false, 0)]
    public static void AddTag_Image()
    {
        AddTag(Const.Tag_Image);
    }

    [MenuItem("GameObject/UI标签/Image/移除标签", false, 0)]
    public static void RemoveTag_Image()
    {
        RemoveTag(Const.Tag_Image);
    }
    #endregion

    #region RawImage
    [MenuItem("GameObject/UI标签/RawImage/添加标签", false, 0)]
    public static void AddTag_RawImage()
    {
        AddTag(Const.Tag_RawImage);
    }

    [MenuItem("GameObject/UI标签/RawImage/移除标签", false, 0)]
    public static void RemoveTag_RawImage()
    {
        RemoveTag(Const.Tag_RawImage);
    }
    #endregion

    #region Button
    [MenuItem("GameObject/UI标签/Button/添加标签", false, 0)]
    public static void AddTag_Button()
    {
        AddTag(Const.Tag_Button);
    }

    [MenuItem("GameObject/UI标签/Button/移除标签", false, 0)]
    public static void RemoveTag_Button()
    {
        RemoveTag(Const.Tag_Button);
    }
    #endregion

    #region Slider
    [MenuItem("GameObject/UI标签/Slider/添加标签", false, 0)]
    public static void AddTag_Slider()
    {
        AddTag(Const.Tag_Slider);
    }

    [MenuItem("GameObject/UI标签/Slider/移除标签", false, 0)]
    public static void RemoveTag_Slider()
    {
        RemoveTag(Const.Tag_Slider);
    }
    #endregion

    #endregion

    [MenuItem("GameObject/UI脚本/CtrlBase/生成脚本", false, 0)]
    public static void CreateCtrlClass()
    {
        UICtrlBaseCreate ctrl = new UICtrlBaseCreate(Selection.activeGameObject);
        if (ctrl.IsLegal)
        {
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Application.dataPath + UIScriptCreatePath, "") + "/";
            if (string.IsNullOrEmpty(folder))
            {
                return;
            }
            string scriptName = ctrl.ClassName;
            string directoryPath = folder;
            string filePath = directoryPath + "/" + scriptName + ".cs";
            if (File.Exists(filePath))
            {
                Debug.Log(scriptName + "已存在，路径为：" + filePath);
                return;
            }
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.Write(ctrl.CreateClass());
            }
            AssetDatabase.Refresh();
        }
    }

    [MenuItem("GameObject/UI脚本/CtrlBase/更新脚本", false, 0)]
    public static void UpdateCtrlClass()
    {
        UICtrlBaseUpdate ctrl = new UICtrlBaseUpdate(Selection.activeGameObject);
        if (ctrl.IsLegal)
        {
            FileInfo file = UIScriptsHelper.FindClassFileInfo(ctrl.ClassName);
            using (StreamWriter writer = new StreamWriter(file.FullName, false, Encoding.UTF8))
            {
                writer.Write(ctrl.UpdateClass(ctrl.ClassStr));
            }
            AssetDatabase.Refresh();
        }
    }
    #region 解析方法
    public static void AddTag(string tag)
    {
        var objs = Selection.gameObjects;
        if (objs != null)
        {
            foreach (var go in objs)
            {
                TagData msg = UIScriptsHelper.ParseName(go);
                if (msg.tags != null && msg.tags.Contains(tag))
                {
                    continue;
                }
                if (msg.tags == null)
                {
                    msg.tags = new HashSet<string>();
                }
                msg.tags.Add(tag);
                go.name = UIScriptsHelper.CombineName(msg);
                var prefabStage = PrefabStageUtility.GetPrefabStage(go);
                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }
            }
        }
    }

    public static void RemoveTag(string tag)
    {
        var objs = Selection.gameObjects;
        if (objs != null)
        {
            foreach (var go in objs)
            {
                TagData msg = UIScriptsHelper.ParseName(go);
                if (msg.tags == null)
                {
                    continue;
                }
                if (msg.tags.Contains(tag) == false)
                {
                    continue;
                }
                msg.tags.Remove(tag);
                go.name = UIScriptsHelper.CombineName(msg);
                var prefabStage = PrefabStageUtility.GetPrefabStage(go);
                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }
            }
        }
    }

    #endregion
}