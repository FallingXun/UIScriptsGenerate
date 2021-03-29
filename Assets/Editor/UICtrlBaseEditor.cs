using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UICtrlBase), true), CanEditMultipleObjects]
public class UICtrlBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        if (GUILayout.Button("绑定脚本对象"))
        {
            if (target is LuaCtrlBase)
            {
                HierarchyEditor.SetLuaUIToInspector(((LuaCtrlBase)target).gameObject);
            }
            else
            {
                HierarchyEditor.SetUIToInspector(((UICtrlBase)target).gameObject);
            }
        }

        if (GUILayout.Button("更新脚本变量"))
        {
            if (target is LuaCtrlBase)
            {
                HierarchyEditor.SetLuaUIToInspector(((LuaCtrlBase)target).gameObject);
            }
            else
            {
                HierarchyEditor.UpdateCtrlClass(((UICtrlBase)target).gameObject);
            }
        }
        EditorGUILayout.Space();
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(UISubCtrlBase), true), CanEditMultipleObjects]
public class UISubCtrlBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        if (GUILayout.Button("绑定脚本对象"))
        {
            if (target is LuaSubCtrlBase)
            {
                HierarchyEditor.SetLuaUIToInspector(((LuaSubCtrlBase)target).gameObject);
            }
            else
            {
                HierarchyEditor.SetUIToInspector(((UISubCtrlBase)target).gameObject);
            }
        }
        if (GUILayout.Button("更新脚本变量"))
        {
            if (target is LuaSubCtrlBase)
            {
                HierarchyEditor.SetLuaUIToInspector(((LuaSubCtrlBase)target).gameObject);
            }
            else
            {
                HierarchyEditor.UpdateSubCtrlClass(((UISubCtrlBase)target).gameObject);
            }
        }
        EditorGUILayout.Space();
        base.OnInspectorGUI();
    }
}
