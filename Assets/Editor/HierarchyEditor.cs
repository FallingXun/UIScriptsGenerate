using System.Collections;
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

    #region Toggle
    [MenuItem("GameObject/UI标签/Toggle/添加标签", false, 0)]
    public static void AddTag_Toggle()
    {
        AddTag(Const.Tag_Toggle);
    }

    [MenuItem("GameObject/UI标签/Toggle/移除标签", false, 0)]
    public static void RemoveTag_Toggle()
    {
        RemoveTag(Const.Tag_Toggle);
    }
    #endregion

    #region Dropdown
    [MenuItem("GameObject/UI标签/Dropdown/添加标签", false, 0)]
    public static void AddTag_Dropdown()
    {
        AddTag(Const.Tag_Dropdown);
    }

    [MenuItem("GameObject/UI标签/Dropdown/移除标签", false, 0)]
    public static void RemoveTag_Dropdown()
    {
        RemoveTag(Const.Tag_Dropdown);
    }
    #endregion

    #region ScrollRect
    [MenuItem("GameObject/UI标签/ScrollRect/添加标签", false, 0)]
    public static void AddTag_ScrollRect()
    {
        AddTag(Const.Tag_ScrollRect);
    }

    [MenuItem("GameObject/UI标签/ScrollRect/移除标签", false, 0)]
    public static void RemoveTag_ScrollRect()
    {
        RemoveTag(Const.Tag_ScrollRect);
    }
    #endregion

    #region InputField
    [MenuItem("GameObject/UI标签/InputField/添加标签", false, 0)]
    public static void AddTag_InputField()
    {
        AddTag(Const.Tag_InputField);
    }

    [MenuItem("GameObject/UI标签/InputField/移除标签", false, 0)]
    public static void RemoveTag_InputField()
    {
        RemoveTag(Const.Tag_InputField);
    }
    #endregion

    #region ReusableLayoutGroup
    [MenuItem("GameObject/UI标签/ReusableLayoutGroup/添加标签", false, 0)]
    public static void AddTag_ReusableLayoutGroup()
    {
        AddTag(Const.Tag_ReusableLayoutGroup);
    }

    [MenuItem("GameObject/UI标签/ReusableLayoutGroup/移除标签", false, 0)]
    public static void RemoveTag_ReusableLayoutGroup()
    {
        RemoveTag(Const.Tag_ReusableLayoutGroup);
    }
    #endregion

    #region Item
    [MenuItem("GameObject/UI标签/Item/添加标签", false, 0)]
    public static void AddTag_Item()
    {
        AddTag(Const.Tag_Item);
    }

    [MenuItem("GameObject/UI标签/Item/移除标签", false, 0)]
    public static void RemoveTag_Item()
    {
        RemoveTag(Const.Tag_Item);
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

        UpdateUIConstClass();
    }

    [MenuItem("GameObject/UI脚本/CtrlBase/更新脚本", false, 0)]
    public static void UpdateCtrlClass()
    {
        UpdateCtrlClass(Selection.activeGameObject);
    }

    public static void UpdateCtrlClass(GameObject go )
    {
        UICtrlBaseUpdate ctrl = new UICtrlBaseUpdate(go);
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

    [MenuItem("GameObject/UI脚本/SubCtrlBase/生成脚本", false, 0)]
    public static void CreateSubCtrlClass()
    {
        UISubCtrlBaseCreate ctrl = new UISubCtrlBaseCreate(Selection.activeGameObject);
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

    [MenuItem("GameObject/UI脚本/SubCtrlBase/更新脚本", false, 0)]
    public static void UpdateSubCtrlClass()
    {
        UpdateSubCtrlClass(Selection.activeGameObject);
    }

    public static void UpdateSubCtrlClass(GameObject go)
    {
        UISubCtrlBaseUpdate ctrl = new UISubCtrlBaseUpdate(go);
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

    [MenuItem("GameObject/UI脚本/ScreenBase/生成脚本", false, 0)]
    public static void CreateScreenClass()
    {
        UIScreenBaseCreate ctrl = new UIScreenBaseCreate(Selection.activeGameObject);
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

    [MenuItem("GameObject/UI脚本/SubScreenBase/生成脚本", false, 0)]
    public static void CreateSubScreenClass()
    {
        UISubScreenBaseCreate ctrl = new UISubScreenBaseCreate(Selection.activeGameObject);
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

    [MenuItem("GameObject/UI脚本/LuaScreenBase/生成脚本", false, 0)]
    public static void CreateLuaScreenClass()
    {
        LuaScreenBaseCreate ctrl = new LuaScreenBaseCreate(Selection.activeGameObject);
        if (ctrl.IsLegal)
        {
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Application.dataPath + UIScriptCreatePath, "") + "/";
            if (string.IsNullOrEmpty(folder))
            {
                return;
            }
            string scriptName = ctrl.ClassName;
            string directoryPath = folder;
            string filePath = directoryPath + "/" + scriptName + ".lua";
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

    [MenuItem("GameObject/UI脚本/LuaSubScreenBase/生成脚本", false, 0)]
    public static void CreateLuaSubScreenClass()
    {
        LuaSubScreenBaseCreate ctrl = new LuaSubScreenBaseCreate(Selection.activeGameObject);
        if (ctrl.IsLegal)
        {
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Application.dataPath + UIScriptCreatePath, "") + "/";
            if (string.IsNullOrEmpty(folder))
            {
                return;
            }
            string scriptName = ctrl.ClassName;
            string directoryPath = folder;
            string filePath = directoryPath + "/" + scriptName + ".lua";
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

    [MenuItem("GameObject/UI脚本/UIConst/添加UI常量", false, 0)]
    public static void UpdateUIConstClass()
    {
        UIConstUpdate ctrl = new UIConstUpdate(Selection.activeGameObject);
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


    [MenuItem("GameObject/UI设置/设置UI组件挂到Inspector", false, 0)]
    public static void SetUIToInspector()
    {
        var go = Selection.activeGameObject;
        SetUIToInspector(go);
    }

    public static void SetUIToInspector(GameObject go)
    {
        InspectorBase inspector = new UIInspectorSetting();
        inspector.Execute(go);
        var prefabStage = PrefabStageUtility.GetPrefabStage(go);
        if (prefabStage != null)
        {
            EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }
    }

    [MenuItem("GameObject/UI设置/设置Lua_UI组件挂到Inspector", false, 0)]
    public static void SetLuaUIToInspector()
    {
        var go = Selection.activeGameObject;
        SetLuaUIToInspector(go);
    }

    public static void SetLuaUIToInspector(GameObject go)
    {
        InspectorBase inspector = new LuaUIInspectorSetting();
        inspector.Execute(go);
        var prefabStage = PrefabStageUtility.GetPrefabStage(go);
        if (prefabStage != null)
        {
            EditorSceneManager.MarkSceneDirty(prefabStage.scene);
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
