using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UITagWindowEditor : EditorWindow
{
    private Vector2 vec = Vector2.zero;

    [MenuItem("UITools/Tag Window")]
    private static void Init()
    {
        UITagWindowEditor window = (UITagWindowEditor)GetWindow(typeof(UITagWindowEditor), false, "Tag Window", true);//创建窗口

        window.Show();
    }

    private void OnGUI()
    {
        vec = GUILayout.BeginScrollView(vec);
        GUILayout.Label("Hierarchy选中对象后，点击\"+\"或\"-\"可以\"添加\"或\"移除\"标签");
        foreach (var item in Const.m_ComponentDict)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("{0} ({1})", item.Value.Name, item.Key), GUILayout.Width(260f));
            if (GUILayout.Button("+", GUILayout.Width(21f)))
            {
                HierarchyEditor.AddTag(item.Key);
            }
            if (GUILayout.Button("-", GUILayout.Width(21f)))
            {
                HierarchyEditor.RemoveTag(item.Key);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
}
