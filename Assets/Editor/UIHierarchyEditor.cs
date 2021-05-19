using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;
using ScriptsGenerate;

public class UIHierarchyEditor : Editor
{
    public const string UIScriptCreatePath = "Assets/Script/Surface/UI/";

    [MenuItem("GameObject/UI脚本/CtrlBase/生成脚本", false, 0)]
    public static void CreateCtrlClass()
    {
        UICtrlBaseCreate ctrl = new UICtrlBaseCreate(Selection.activeGameObject);
        if (ctrl.IsLegal)
        {
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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

    public static void UpdateCtrlClass(GameObject go)
    {
        UICtrlBaseUpdate ctrl = new UICtrlBaseUpdate(go);
        if (ctrl.IsLegal)
        {
            FileInfo file = ScriptsHelper.FindClassFileInfo(ctrl.ClassName);
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
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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
            FileInfo file = ScriptsHelper.FindClassFileInfo(ctrl.ClassName);
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
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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
            string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Path.Combine(Directory.GetCurrentDirectory(), UIScriptCreatePath), "") + "/";
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
            FileInfo file = ScriptsHelper.FindClassFileInfo(ctrl.ClassName);
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

}
