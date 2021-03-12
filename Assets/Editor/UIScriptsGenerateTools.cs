using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class UIScriptsGenerateTools : EditorWindow
{
#if !UNITY_EDITOR
#region UI自动生成功能
    private const string UICtrlInheritType = "UICtrlBase";
    private const string UISubCtrlInheritSubType = "UISubCtrlBase";
    private const string UICtrlEndType = "Ctrl";
    private const string UISubCtrlEndType = "SubCtrl";
    private const string UIScreenInheritType = "UICtrlBase";
    private const string UISubScreenInheritSubType = "UISubCtrlBase";
    private const string UIScreenEndType = "Screen";
    private const string UISubScreenEndType = "SubScreen";

    private const string UIOpenParamType = "UIOpenScreenParameterBase";

    private const string STR_GameObject = "go";
    private const string STR_Transform = "tf";
    private const string STR_TextMeshProUGUI = "text";
    private const string STR_Image = "img";
    private const string STR_RawImage = "rimg";
    private const string STR_Button = "btn";
    private const string STR_Slider = "sd";

    //需要自动设置的组件，脚本名需要以Ctrl结尾，代码定义名称格式为:变量名+"_"+typeStr中的类型，unity中组件名格式为:代码变量名+"|"+typeStr中的类型
    static string[] typeStr =
    {
        STR_GameObject,
        STR_Transform,
        STR_TextMeshProUGUI,
        STR_Image,
        STR_RawImage,
        STR_Button,
        STR_Slider,
        //"pref"/*GameObject，动态生成的预制体*/,
    };

    static string[] ctrlUsingStr =
    {
        "using System;",
        "using System.Collections.Generic;",
        "using UnityEngine;",
        "using TMPro;",
        "using UnityEngine.UI;",
    };

    static string[] screenUsingStr =
    {
        "using System;",
        "using System.Collections.Generic;",
        "using CnfSprotoType;",
    };

    [MenuItem("UITools/UI自动生成工具/生成面板对应Ctrl脚本")]
    static void CreateUICtrlScript()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Application.dataPath + "/Script/Surface/UI/", "") + "/";
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        string scriptName = parentTf.name;
        if (!scriptName.EndsWith(UICtrlEndType))
        {
            Debug.LogError(string.Format("当前选择的物体命名不为{0}或{1}结尾", UICtrlEndType, UISubCtrlEndType));
            return;
        }
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
        StreamWriter DataStruct = new StreamWriter(directoryPath + "/" + scriptName + ".cs", false, Encoding.UTF8);
        for (int i = 0; i < ctrlUsingStr.Length; i++)
        {
            DataStruct.WriteLine(ctrlUsingStr[i]);
        }
        DataStruct.WriteLine();
        DataStruct.WriteLine();
        string inheritType = string.Empty;
        if (scriptName.EndsWith(UISubCtrlEndType))
        {
            inheritType = UISubCtrlInheritSubType;
        }
        else
        {
            inheritType = UICtrlInheritType;
        }
        DataStruct.WriteLine(string.Format("public class {0} : {1}", scriptName, inheritType));
        DataStruct.WriteLine("{");
        DataStruct.WriteLine("    #region 工具生成变量");
        for (int i = 0; i < tfs.Length; i++)
        {
            string curType = GetTransType(tfs[i].name);
            if (string.IsNullOrEmpty(curType))
            {
                continue;
            }
            DataStruct.WriteLine(GetObjDefine(tfs[i], curType));
        }
        DataStruct.WriteLine("    #endregion");
        DataStruct.WriteLine();
        DataStruct.WriteLine("}");
        DataStruct.Close();
        AssetDatabase.Refresh();
    }

    static void CreateUIScreenScript()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        string folder = EditorUtility.OpenFolderPanel("选择脚本生成的文件夹", Application.dataPath + "/Script/Surface/UI/", "") + "/";
        string scriptName = "";
        var ctrl = parentTf.GetComponent<UICtrlBase>();
        var subCtrl = parentTf.GetComponent<UISubCtrlBase>();
        string screenType = "";
        if (ctrl != null)
        {
            screenType = UIScreenEndType;
        }
        else if (subCtrl != null)
        {
            screenType = UISubScreenEndType;
        }
        if (string.IsNullOrEmpty(screenType))
        {
            Debug.LogError(string.Format("当前选择的物体不为{0}或{1}", UICtrlInheritType, UISubCtrlInheritSubType));
            return;
        }
        string ctrlType = ctrl.GetType().ToString();
        scriptName = ctrlType.Substring(0, ctrlType.Length - UICtrlEndType.Length) + screenType;
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
        StreamWriter DataStruct = new StreamWriter(directoryPath + "/" + scriptName + ".cs", false, Encoding.UTF8);
        for (int i = 0; i < screenUsingStr.Length; i++)
        {
            DataStruct.WriteLine(screenUsingStr[i]);
        }
        DataStruct.WriteLine();
        DataStruct.WriteLine();
        string inheritType = string.Empty;
        if (scriptName.EndsWith(UISubScreenEndType))
        {
            inheritType = UISubScreenInheritSubType;
        }
        else
        {
            inheritType = UIScreenInheritType;
        }
        DataStruct.WriteLine(string.Format("public class {0} : {1}", scriptName, inheritType));
        DataStruct.WriteLine("{");
        DataStruct.WriteLine(string.Format("    public {0}({1} param) : base(UIConst.UI{2}, param)", scriptName, UIOpenParamType, scriptName));
        DataStruct.WriteLine("    {");
        DataStruct.WriteLine("    }");
        DataStruct.WriteLine();
        DataStruct.WriteLine();
        DataStruct.WriteLine("    #region 工具生成变量");
        DataStruct.WriteLine(string.Format("    private {0} m_Ctrl;", ctrlType));
        DataStruct.WriteLine("    #endregion");
        DataStruct.WriteLine();
        DataStruct.WriteLine("    }");
        DataStruct.Close();
        AssetDatabase.Refresh();
    }


    static string GetTransType(string tfName)
    {
        if (tfName.Contains("|"))
        {
            return GetNameType(tfName.Replace("|", "_"));
        }
        return "";
    }

    static string GetObjDefine(Transform tf, string typeName)
    {
        string definition = "    public ";
        switch (typeName)
        {
            case STR_GameObject:
                {
                    definition += "GameObject ";
                }
                break;
            case STR_Transform:
                {
                    definition += "Transform ";
                }
                break;
            case STR_TextMeshProUGUI:
                {
                    definition += "TextMeshProUGUI ";
                }
                break;
            case STR_Image:
                {
                    definition += "Image ";
                }
                break;
            case STR_RawImage:
                {
                    definition += "RawImage ";
                }
                break;
            case STR_Button:
                {
                    definition += "Button ";
                }
                break;
            case STR_Slider:
                {
                    definition += "Slider ";
                }
                break;
            //case "pref":
            //    {
            //        definition += "GameObject ";
            //    }
            //    break;
            default:
                Debug.LogError("无法生成" + tf.name + "的定义!!!");
                return "";
        }
        definition += "m_" + GetFirstCharDown(tf.name).Replace("|", "_");
        definition += ";";
        return definition;
    }

    private static string GetButtonClickEvent(List<string> clickEvent, Type curType)
    {
        string definition = "";
        for (int i = 0; i < clickEvent.Count; i++)
        {
            if (CheckMethodExist(curType, clickEvent[i]))
            {
                Debug.Log("已存在" + GetClickFuncName(clickEvent[i]) + "方法！！！！");
                continue;
            }
            definition += "    public void ";
            string eventName = GetClickFuncName(clickEvent[i]);
            eventName += "()";
            definition += eventName;
            definition += "\n    {\n        \n    }\n\n";
        }
        if (curType == null || !CheckMethodExist(curType, "PlaySound"))
        {
            // 音效方法
            definition += "    public void PlaySound()\n";
            definition += "    {\n";
            definition += "        Global.OpenCloseSound();\n";
            definition += "    }";
            definition += "\n\n";
        }
        else
        {
            Debug.Log("已存在PlaySound方法！！！！");
        }
        return definition;
    }

    static bool CheckMethodExist(Type curType, string methodName)
    {
        if (curType != null)
        {
            MethodInfo[] infos = curType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < infos.Length; i++)
            {
                if (methodName == infos[i].Name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    static string GetClickFuncName(string tfName)
    {
        return GetFirstCharUp(tfName.Split('|')[0].Replace("button", "").Replace("btn", "").Replace("Click", "")) + "Click";
    }

    [MenuItem("UITools/UI自动生成工具/新增组件更新到对应Ctrl脚本")]
    public static void UpdatePanelDataLuaScript()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        MonoBehaviour[] monos = parentTf.GetComponents<MonoBehaviour>();
        Transform mono = null;
        string className = null;
        System.Type curType = null;
        for (int i = 0; i < monos.Length; i++)
        {
            var uiType = monos[i].GetType();

            string curName = monos[i].GetType().ToString();
            if (curName.EndsWith("Ctrl"))
            {
                curType = monos[i].GetType();
                className = curName;
                mono = monos[i].transform;
                break;
            }
        }
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        List<string> clickEvent = new List<string>();
        List<string> variableDefineAddList = new List<string>();
        string functionDefineAdd = "";
        for (int i = 0; i < tfs.Length; i++)
        {
            string tfType = GetTransType(tfs[i].name);
            if (string.IsNullOrEmpty(tfType))
            {
                continue;
            }
            //检查是否已存在该变量
            FieldInfo info = curType.GetField(GetFirstCharDown(tfs[i].name).Replace("|", "_"));
            if (info != null)
            {
                continue;
            }
            variableDefineAddList.Add(GetObjDefine(tfs[i], tfType, ref clickEvent));
        }
        functionDefineAdd = GetButtonClickEvent(clickEvent, curType);

        FileInfo file = FindClassFileInfo(className);
        if (file != null)
        {
            string allText = "";
            bool hasError = false;
            bool hasAdd = false;
            bool hasClickRegion = false;
            try
            {
                StreamReader readerOld = file.OpenText();
                string allLine = readerOld.ReadToEnd();
                readerOld.Close();
                if (allLine.Contains("工具生成点击事件"))
                {
                    hasClickRegion = true;
                }
                StreamReader reader = file.OpenText();
                if (!allLine.Contains("工具生成变量"))    //旧脚本
                {
                    bool clickDefine = false;
                    bool variableDefine = false;
                    string curLine = reader.ReadLine();
                    bool find = false;      //找到类对应行
                    string regionVariable = "";
                    string regionClick = "";
                    while (curLine != null)
                    {
                        if (!find && curLine.Contains("public ") && curLine.Contains("class ") && (curLine.Contains(className + " ") || curLine.Contains(className + "(")))
                        {
                            find = true;
                        }
                        if (find)
                        {
                            if (!variableDefine && curLine.Contains("{"))
                            {
                                regionVariable += "\n";
                                regionVariable += "    #region 工具生成变量";
                                regionVariable += "\n";
                                for (int i = 0; i < variableDefineAddList.Count; i++)
                                {
                                    regionVariable += variableDefineAddList[i];
                                    regionVariable += "\n";
                                    Debug.Log("新增组件定义,具体定义为：" + variableDefineAddList[i]);
                                    hasAdd = true;
                                }
                                variableDefineAddList.Clear();
                                regionVariable += "    #endregion";
                                regionVariable += "\n";
                                variableDefine = true;
                            }
                            if (!hasClickRegion)    //无工具生成变量，无工具生成点击事件，需重新生成
                            {
                                if (!clickDefine && (curLine.Contains("private ") || curLine.Contains("public ") || curLine.Contains(" void ")) && curLine.Contains("(") && curLine.Contains(")"))
                                {
                                    regionClick = "\n";
                                    regionClick += "    #region 工具生成点击事件";
                                    regionClick += "\n";
                                    if (!string.IsNullOrEmpty(functionDefineAdd))
                                    {
                                        regionClick += "\n";
                                        regionClick += functionDefineAdd;
                                        Debug.Log("新增点击事件定义,具体定义为：" + functionDefineAdd.Replace("{", "").Replace("}", "").Replace("\n", "").Replace("()", "\n"));
                                        hasAdd = true;
                                    }
                                    functionDefineAdd = "";
                                    regionClick += "    #endregion";
                                    regionClick += "\n\n";
                                    clickDefine = true;
                                }
                            }
                            else                //无工具生成变量，有工具生成点击事件，需往工具生成点击事件中添加
                            {
                                if (!clickDefine && curLine.Contains("工具生成点击事件"))
                                {
                                    if (!string.IsNullOrEmpty(functionDefineAdd))
                                    {
                                        regionClick = "\n";
                                        regionClick += functionDefineAdd;
                                        Debug.Log("新增点击事件定义,具体定义为：" + functionDefineAdd.Replace("{", "").Replace("}", "").Replace("\n", "").Replace("()", "\n"));
                                        hasAdd = true;
                                    }
                                    clickDefine = true;
                                }
                            }
                        }
                        if (!hasClickRegion && !string.IsNullOrEmpty(regionClick) && clickDefine)   //无工具生成变量，无工具生成点击事件，需重新生成
                        {
                            allText += regionClick;
                            regionClick = "";
                        }
                        allText += curLine;
                        allText += "\n";
                        if (hasClickRegion && !string.IsNullOrEmpty(regionClick) && clickDefine)     //无工具生成变量，有工具生成点击事件，需往工具生成点击事件中添加
                        {
                            allText += regionClick;
                            regionClick = "";
                        }
                        if (!string.IsNullOrEmpty(regionVariable) && variableDefine)
                        {
                            allText += regionVariable;
                            regionVariable = "";
                        }
                        curLine = reader.ReadLine();
                    }
                }
                else
                {
                    bool variableDefine = true; //已经有工具生成变量
                    bool clickDefine = false;
                    bool find = false;      //找到类对应行
                    string regionVariable = "";
                    string regionClick = "";
                    string curLine = reader.ReadLine();
                    while (curLine != null)
                    {
                        if (curLine.Contains("工具生成变量"))
                        {
                            for (int i = 0; i < variableDefineAddList.Count; i++)
                            {
                                regionVariable += variableDefineAddList[i];
                                regionVariable += "\n";
                                Debug.Log("新增组件定义,具体定义为：" + variableDefineAddList[i]);
                                hasAdd = true;
                            }
                            variableDefineAddList.Clear();
                        }
                        else if (!string.IsNullOrEmpty(functionDefineAdd))   //有添加点击方法
                        {
                            if (hasClickRegion)     //有工具生成变量，有工具生成点击事件，需往工具生成点击事件中添加
                            {
                                if (curLine.Contains("工具生成点击事件"))
                                {
                                    if (!string.IsNullOrEmpty(functionDefineAdd))
                                    {
                                        regionClick += "\n";
                                        regionClick += functionDefineAdd;
                                        Debug.Log("新增点击事件定义,具体定义为：" + functionDefineAdd.Replace("{", "").Replace("}", "").Replace("\n", "").Replace("()", "\n"));
                                        hasAdd = true;
                                    }
                                    clickDefine = true;
                                }
                            }
                            else        //有工具生成变量，无工具生成点击事件，需重新生成
                            {
                                if (!find && curLine.Contains("public ") && curLine.Contains("class ") && (curLine.Contains(className + " ") || curLine.Contains(className + "(")))
                                {
                                    find = true;
                                }
                                if (find)
                                {
                                    if (!clickDefine && (curLine.Contains("private ") || curLine.Contains("public ") || curLine.Contains(" void ")) && curLine.Contains("(") && curLine.Contains(")"))
                                    {
                                        regionClick = "\n";
                                        regionClick += "    #region 工具生成点击事件";
                                        regionClick += "\n";
                                        if (!string.IsNullOrEmpty(functionDefineAdd))
                                        {
                                            regionClick += "\n";
                                            regionClick += functionDefineAdd;
                                            Debug.Log("新增点击事件定义,具体定义为：" + functionDefineAdd.Replace("{", "").Replace("}", "").Replace("\n", "").Replace("()", "\n"));
                                            hasAdd = true;
                                        }
                                        functionDefineAdd = "";
                                        regionClick += "    #endregion";
                                        regionClick += "\n\n";
                                        clickDefine = true;
                                    }
                                }
                            }
                        }
                        if (!hasClickRegion && !string.IsNullOrEmpty(regionClick) && clickDefine)   //有工具生成变量，无工具生成点击事件，需重新生成
                        {
                            allText += regionClick;
                            regionClick = "";
                        }
                        allText += curLine;
                        allText += "\n";
                        if (hasClickRegion && !string.IsNullOrEmpty(regionClick) && clickDefine)     //有工具生成变量，有工具生成点击事件，需往工具生成点击事件中添加
                        {
                            allText += regionClick;
                            regionClick = "";
                        }
                        if (!string.IsNullOrEmpty(regionVariable) && variableDefine)
                        {
                            allText += regionVariable;
                            regionVariable = "";
                        }
                        curLine = reader.ReadLine();
                    }
                }
                reader.Close();
            }

            catch (IOException e)
            {
                hasError = true;
                Debug.LogError("读取脚本文件出错：" + e);
            }
            if (!hasError)
            {
                if (hasAdd)
                {
                    if (File.Exists(file.DirectoryName + "/" + className + ".cs"))
                    {
                        string[] str = allText.Split('\n');
                        File.WriteAllLines(file.DirectoryName + "/" + className + ".cs", str, Encoding.UTF8);
                        Debug.Log("新增变量和方法成功写入" + className + ".cs！！！！");
                        AssetDatabase.Refresh();
                        return;
                    }
                }
                else
                {
                    Debug.Log("未发现有新增组件！！！！！！！！");
                    return;
                }

            }
        }
        Debug.LogError("未能更新脚本，请检查！！！！！！！！！！！");
    }

    private static FileInfo FindClassFileInfo(string className)
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


    [MenuItem("UITools/UI自动生成工具/设置UI组件挂到Inspector")]
    public static void SetUIToInspector()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        MonoBehaviour[] monos = parentTf.GetComponents<MonoBehaviour>();
        Transform mono = null;
        string className = null;
        Type curType = null;
        for (int i = 0; i < monos.Length; i++)
        {
            string curName = monos[i].GetType().ToString();
            if (curName.EndsWith("Ctrl"))
            {
                curType = monos[i].GetType();
                className = curName;
                mono = monos[i].transform;
                break;
            }
        }
        if (curType == null)
        {
            Debug.LogError("当前选中对象不存在Ctrl脚本，请检查！！！");
            return;
        }
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        if (curType != null)
        {
            System.Reflection.FieldInfo[] info = curType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < info.Length; i++)
            {
                Debug.Log("当前变量名：" + info[i].Name);
                string paramType = GetNameType(info[i].Name);
                if (!string.IsNullOrEmpty(paramType))
                {
                    object obj = null;
                    if (paramType == "pref")
                    {
                        bool find = false;
                        List<GameObject> objs = FindPathGameObj("Assets/ResourcesAsset/UI");
                        //下面就可以对该资源做任何你想要的操作了，如查找已丢失的脚本、检查赋值命名等，这里查找所有的Text组件个数
                        for (int j = 0; j < objs.Count; j++)
                        {
                            if (GetFirstCharDown(objs[j].name) == GetPrefName(info[i].Name))
                            {
                                info[i].SetValue(mono.GetComponent(curType), objs[j]);
                                Debug.Log("成功设置预制体：" + info[i].Name);
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            List<GameObject> objs1 = FindPathGameObj("Assets/ResourcesAsset");
                            for (int j = 0; j < objs1.Count; j++)
                            {
                                if (GetFirstCharDown(objs1[j].name) == GetPrefName(info[i].Name))
                                {
                                    info[i].SetValue(mono.GetComponent(curType), objs1[j]);
                                    Debug.Log("成功设置预制体：" + info[i].Name);
                                    find = true;
                                    break;
                                }
                            }
                        }
                        if (!find)
                        {
                            List<GameObject> objs2 = FindPathGameObj("Assets/Resources");
                            for (int j = 0; j < objs2.Count; j++)
                            {
                                if (GetFirstCharDown(objs2[j].name) == GetPrefName(info[i].Name))
                                {
                                    info[i].SetValue(mono.GetComponent(curType), objs2[j]);
                                    Debug.Log("成功设置预制体：" + info[i].Name);
                                    find = true;
                                    break;
                                }
                            }
                        }
                        if (!find)
                        {
                            Debug.LogError("未找到名为" + GetPrefName(info[i].Name) + "的预制体，请检查！！！！");
                        }

                    }
                    else
                    {
                        obj = GetObj(info[i].Name, tfs, paramType, parentTf, curType);
                    }
                    if (obj != null)
                    {
                        info[i].SetValue(mono.GetComponent(curType), obj);
                        Debug.Log("成功设置：" + info[i].Name);
                    }
                }
            }
        }
    }

    private static List<GameObject> FindPathGameObj(string assetPath)
    {
        //查找指定路径下指定类型的所有资源，返回的是资源GUID
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new string[] { assetPath });
        //从GUID获得资源所在路径
        List<string> paths = new List<string>();
        guids.ToList().ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
        //从路径获得该资源
        List<GameObject> objs = new List<GameObject>();
        paths.ForEach(p => objs.Add(AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject));
        return objs;
    }

    static object GetObj(string n, Transform[] tfs, string typeName, Transform parent, Type curType)
    {
        for (int i = 0; i < tfs.Length; i++)
        {
            if (GetFirstCharDown(tfs[i].name.Replace("|", "_")) == n)
            {
                switch (typeName)
                {
                    case "go":
                        {
                            return tfs[i].gameObject;
                        }
                    case "tf":
                        {
                            return tfs[i].transform;
                        }
                    case "lbl":
                        {
                            UILabel lbl = tfs[i].GetComponent<UILabel>();
                            if (lbl == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UILabel 组件，请检查！！！");
                            }
                            return lbl;
                        }
                    case "spr":
                        {
                            UISprite spr = tfs[i].GetComponent<UISprite>();
                            if (spr == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UISpirte 组件，请检查！！！");
                            }
                            return spr;
                        }
                    case "tex":
                        {
                            UITexture tex = tfs[i].GetComponent<UITexture>();
                            if (tex == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UITexture 组件，请检查！！！");
                            }
                            return tex;
                        }
                    case "grid":
                        {
                            UIGrid grid = tfs[i].GetComponent<UIGrid>();
                            if (grid == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UIGrid 组件，请检查！！！");
                            }
                            return grid;
                        }
                    case "sv":
                        {
                            UIScrollView sv = tfs[i].GetComponent<UIScrollView>();
                            if (sv == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UIScrollView 组件，请检查！！！");
                            }
                            return sv;
                        }
                    case "sbar":
                        {
                            UIScrollBar sbar = tfs[i].GetComponent<UIScrollBar>();
                            if (sbar == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UIScrollBar 组件，请检查！！！");
                            }
                            return sbar;
                        }
                    case "tb":
                        {
                            UITable tb = tfs[i].GetComponent<UITable>();
                            if (tb == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UITable 组件，请检查！！！");
                            }
                            return tb;
                        }
                    case "panel":
                        {
                            UIPanel panel = tfs[i].GetComponent<UIPanel>();
                            if (panel == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UIPanel 组件，请检查！！！");
                            }
                            return panel;
                        }
                    case "sd":
                        {
                            UISlider sd = tfs[i].GetComponent<UISlider>();
                            if (sd == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UISlider 组件，请检查！！！");
                            }
                            return sd;
                        }
                    case "btn":
                        {
                            UIButton btn = tfs[i].GetComponent<UIButton>();
                            if (btn == null)
                            {
                                btn = tfs[i].gameObject.AddComponent<UIButton>();

                            }
                            BoxCollider box = tfs[i].GetComponent<BoxCollider>();
                            if (box == null)
                            {
                                tfs[i].gameObject.AddComponent<BoxCollider>();
                                NGUITools.UpdateWidgetCollider(tfs[i].gameObject);
                            }
                            //AFButtonScale scale = tfs[i].GetComponent<AFButtonScale>();
                            //if (scale == null && tfs[i].name.Contains("Button"))
                            //{
                            //    tfs[i].gameObject.AddComponent<AFButtonScale>();
                            //}
                            if (curType != null)
                            {
                                string className = curType.ToString();
                                List<EventDelegate> del = new List<EventDelegate>();
                                EventDelegate userEvent = new EventDelegate();
                                if (CheckMethodExist(curType, GetClickFuncName(tfs[i].name)))
                                {
                                    userEvent.Set(parent.GetComponent(className) as MonoBehaviour, GetClickFuncName(tfs[i].name));
                                }
                                else
                                {
                                    Debug.LogError(className + "中不存在" + GetClickFuncName(tfs[i].name) + "方法，请检查！！！");
                                }
                                del.Add(userEvent);
                                EventDelegate soundEvent = new EventDelegate();
                                if (CheckMethodExist(curType, "PlaySound"))
                                {
                                    soundEvent.Set(parent.GetComponent(className) as MonoBehaviour, "PlaySound");
                                }
                                else
                                {
                                    Debug.LogError(className + "中不存在PlaySound方法，请检查！！！");
                                }
                                del.Add(soundEvent);
                                btn.onClick.Clear();
                                btn.onClick = del;
                            }
                            Debug.Log(parent.GetType().ToString());
                            return btn;
                        }
                    case "ip":
                        {
                            UIInput ip = tfs[i].GetComponent<UIInput>();
                            if (ip == null)
                            {
                                Debug.LogError(tfs[i].name + "不存在 UIInput 组件，请检查！！！");
                            }
                            return ip;
                        }

                }
                Debug.LogError(tfs[i].name + "的名称不符合自动生成的命名规则，请检查！！！");
                return null;
            }
        }
        int index = n.LastIndexOf('_');
        n = n.Remove(index, 1);
        n = n.Insert(index, "|");
        Debug.LogError("当前选择的面板没有" + n + "，请检查！！！");
        return null;
    }


    static string GetNameType(string mName)
    {
        if (mName.Contains("_"))
        {
            string[] s = mName.Split('_');
            string str = s[s.Length - 1];
            for (int i = 0; i < typeStr.Length; i++)
            {
                if (str == typeStr[i])
                {
                    return typeStr[i];
                }
            }
        }
        return "";
    }

    static string GetFirstCharUp(string curName)
    {
        return curName.Substring(0, 1).ToUpper() + curName.Substring(1, curName.Length - 1);
    }

    static string GetFirstCharDown(string curName)
    {
        return curName.Substring(0, 1).ToLower() + curName.Substring(1, curName.Length - 1);
    }

    static string GetPrefName(string defineName)
    {
        if (defineName.Contains("_"))
        {
            string[] s = defineName.Split('_');
            string str = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                str += s[i];
            }
            return GetFirstCharDown(str);
        }
        return "";
    }


    [MenuItem("UITools/UI自动生成工具/Lua用 生成面板对应Lua脚本")]
    static void CreatePanelLuaScript()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        LuaBehaviour lua = parentTf.GetComponent<LuaBehaviour>();
        if (lua != null)
        {
            Debug.LogError("当前Panel已经存在LuaBehaviour脚本，请使用更新工具！");
            return;
        }
        //PlayerPrefs.SetString("UIOneKeyCreatePath", folder);
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        string scriptName = GetFirstCharUp(parentTf.name) + "Lua.lua";
        string directoryPath = Application.dataPath + "/HotFix/LuaScript/";
        string folder = EditorUtility.OpenFolderPanel("选择lua脚本生成的文件夹", directoryPath, "") + "/";
        Debug.Log("生成lua脚本" + folder);
        List<string> variableList = new List<string>();
        List<string> variableInitList = new List<string>();
        List<string> functionList = new List<string>();
        List<string> clickInitList = new List<string>();
        for (int i = 0; i < tfs.Length; i++)
        {
            if (tfs[i].name.EndsWith("|lua") && variableList.Contains(tfs[i].name) == false)
            {
                variableList.Add(GetFirstCharDown(tfs[i].name));
                if (tfs[i].name.Contains("|btn"))
                {
                    string func = GetFirstCharUp(tfs[i].name.Split('|')[0]) + "OnClick";
                    if (functionList.Contains(func) == false)
                    {
                        functionList.Add(func);
                        clickInitList.Add("CS.UIEventListener.Get(" + GetFirstCharDown(tfs[i].name).Replace("|", "_") + ").onClick = " + func);
                    }
                }
            }
        }
        StreamWriter DataStruct = new StreamWriter(folder + "/" + scriptName + ".txt", false, Encoding.UTF8);
        DataStruct.WriteLine("----------------工具生成变量 start -------------------");
        variableInitList.Add("luaBehaviour = this:GetComponent(typeof(CS.LuaBehaviour))");
        variableInitList.Add("luaGlobal = CS.UIMsgReceive.instance.luaglobaldataBehaviour.scriptTable");
        DataStruct.WriteLine("local luaGlobal = nil");
        DataStruct.WriteLine("local luaBehaviour = nil");
        for (int i = 0; i < variableList.Count; i++)
        {
            string varName = variableList[i].Replace("|lua", "");
            DataStruct.WriteLine("local " + varName.Replace("|", "_") + " = nil");
            variableInitList.Add(GetLuaVariableInit(varName));
        }
        DataStruct.WriteLine("----------------工具生成变量 end -------------------");

        DataStruct.WriteLine();

        DataStruct.WriteLine("function Awake()");
        DataStruct.WriteLine("----------------工具生成变量初始化 start -------------------");
        for (int i = 0; i < variableInitList.Count; i++)
        {
            DataStruct.WriteLine("    " + variableInitList[i]);
        }
        DataStruct.WriteLine("----------------工具生成变量初始化 end -------------------");

        DataStruct.WriteLine();

        DataStruct.WriteLine("----------------工具生成点击事件监听 start -------------------");
        for (int i = 0; i < clickInitList.Count; i++)
        {
            DataStruct.WriteLine("    " + clickInitList[i]);
        }
        DataStruct.WriteLine("----------------工具生成点击事件监听 end -------------------");

        DataStruct.WriteLine("end");

        DataStruct.WriteLine();

        DataStruct.WriteLine("function Start()");
        DataStruct.WriteLine();
        DataStruct.WriteLine("end");

        DataStruct.WriteLine();

        DataStruct.WriteLine("function OnDestroy()");
        DataStruct.WriteLine();
        DataStruct.WriteLine("end");

        DataStruct.WriteLine();
        DataStruct.WriteLine("----------------工具生成点击事件 start -------------------");
        for (int i = 0; i < functionList.Count; i++)
        {
            DataStruct.WriteLine("function " + functionList[i] + " ()");
            DataStruct.WriteLine();
            DataStruct.WriteLine("end");
            DataStruct.WriteLine();
        }
        DataStruct.WriteLine("----------------工具生成点击事件 end -------------------");
        DataStruct.Close();
        //SetUIToLuaBehaviour();
        AssetDatabase.Refresh();
    }

    [MenuItem("UITools/UI自动生成工具/Lua用 新增组件更新到对应lua脚本")]
    public static void UpdatePanelDataScript()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        string scriptName = GetFirstCharUp(parentTf.name) + "Lua.lua";
        string directoryPath = Application.dataPath + "/HotFix/LuaScript/";
        string fileName = directoryPath + parentTf.name + "/" + scriptName + ".txt";
        if (File.Exists(fileName) == false)
        {
            fileName = EditorUtility.OpenFilePanel("选择需要更新的lua脚本", directoryPath, "txt");
        }
        Debug.Log("更新lua脚本" + fileName);
        FileInfo file = new FileInfo(fileName);
        if (file == null)
        {
            Debug.LogError("未找到lua脚本====> " + fileName);
            return;
        }
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        List<string> variableList = new List<string>();
        List<string> variableInitList = new List<string>();
        List<string> functionList = new List<string>();
        List<string> clickInitList = new List<string>();
        variableInitList.Add("luaBehaviour = this:GetComponent(typeof(CS.LuaBehaviour))");
        variableInitList.Add("luaGlobal = CS.UIMsgReceive.instance.luaglobaldataBehaviour.scriptTable");
        variableList.Add("luaGlobal");
        variableList.Add("luaBehaviour");
        for (int i = 0; i < tfs.Length; i++)
        {
            if (tfs[i].name.EndsWith("|lua") && variableList.Contains(GetFirstCharDown(tfs[i].name)) == false)
            {
                string varName = GetFirstCharDown(tfs[i].name).Replace("|lua", "");
                variableList.Add(varName.Replace("|", "_"));
                if (tfs[i].name.Contains("|btn"))
                {
                    string func = GetFirstCharUp(tfs[i].name.Split('|')[0]) + "OnClick";
                    if (functionList.Contains(func) == false)
                    {
                        functionList.Add(func);
                        clickInitList.Add("CS.UIEventListener.Get(" + GetFirstCharDown(tfs[i].name).Replace("|", "_") + ").onClick = " + func);
                    }
                }
                variableInitList.Add(GetLuaVariableInit(varName));

            }
        }
        string allText = "";
        bool hasError = false;
        try
        {
            StreamReader readerOld = file.OpenText();
            string allLine = readerOld.ReadToEnd();
            readerOld.Close();
            StreamReader reader = file.OpenText();
            bool parseVariable = false;
            bool parseFunction = false;
            bool parseVariableInit = false;
            bool parseClickInit = false;
            bool hasClickFunction = false;
            if (allLine.Contains("工具生成点击事件"))
            {
                hasClickFunction = true;
            }
            if (allLine.Contains("工具生成变量"))
            {
                string curLine = reader.ReadLine();
                while (curLine != null)
                {
                    if (string.IsNullOrEmpty(curLine.Trim()) == true)
                    {
                        curLine = reader.ReadLine();
                        allText += "\n";
                        continue;
                    }
                    if (curLine.Contains("工具生成变量 start"))
                    {
                        parseVariable = true;
                    }
                    else if (curLine.Contains("工具生成变量 end"))
                    {
                        for (int i = 0; i < variableList.Count; i++)
                        {
                            allText += "local " + variableList[i] + " = nil";
                            allText += "\n";
                        }
                        parseVariable = false;
                    }
                    else
                    {
                        if (parseVariable == true)
                        {
                            if (curLine.Contains("_pref"))
                            {
                                variableInitList.Add(GetLuaVariableInit(curLine.Substring(0, curLine.IndexOf("=") - 1).Replace("local", "").Trim().Replace("_", "|")));
                            }
                            if (curLine.Contains("local ") && curLine.Contains("="))
                            {
                                string curVariable = curLine.Substring(0, curLine.IndexOf("=") - 1).Replace("local", "").Trim();
                                if (variableList.Contains(curVariable) == true)
                                {
                                    variableList.Remove(curVariable);
                                }
                            }
                        }
                    }
                    if (hasClickFunction)
                    {
                        if (curLine.Contains("工具生成点击事件 start"))
                        {
                            parseFunction = true;
                        }
                        else if (curLine.Contains("工具生成点击事件 end"))
                        {
                            for (int i = 0; i < functionList.Count; i++)
                            {
                                allText += "function " + functionList[i] + " ()";
                                allText += "\n";
                                allText += "end";
                                allText += "\n";
                                Debug.Log("lua新增工具生成点击事件： " + functionList[i]);
                            }
                            parseFunction = false;
                        }
                        else
                        {
                            if (parseFunction == true)
                            {
                                if (curLine.Trim().StartsWith("function ") && curLine.Replace(" ", "").Contains("()"))
                                {
                                    string curFunction = curLine.Substring(0, curLine.IndexOf("(")).Replace("function", "").Trim();
                                    if (functionList.Contains(curFunction) == true)
                                    {
                                        functionList.Remove(curFunction);
                                    }
                                }
                            }
                        }
                    }
                    if (curLine.Contains("工具生成变量初始化 start"))
                    {
                        parseVariableInit = true;
                    }
                    else if (curLine.Contains("工具生成变量初始化 end"))
                    {
                        for (int i = 0; i < variableInitList.Count; i++)
                        {
                            allText += "	" + variableInitList[i];
                            allText += "\n";
                            Debug.Log("lua新增工具生成变量初始化： " + variableInitList[i]);
                        }
                        parseVariableInit = false;
                    }
                    else
                    {
                        if (parseVariableInit == true)
                        {
                            for (int j = 0; j < variableInitList.Count; j++)
                            {
                                if (variableInitList[j].Contains(curLine.Substring(0, curLine.IndexOf('=')).Trim().Replace(" ", "") + " "))
                                {
                                    variableInitList.Remove(variableInitList[j]);
                                    break;
                                }
                            }
                        }
                    }
                    if (curLine.Contains("工具生成点击事件监听 start"))
                    {
                        parseClickInit = true;
                    }
                    else if (curLine.Contains("工具生成点击事件监听 end"))
                    {
                        for (int i = 0; i < clickInitList.Count; i++)
                        {
                            allText += "    " + clickInitList[i];
                            allText += "\n";
                            Debug.Log("lua新增工具生成点击事件监听： " + clickInitList[i]);
                        }
                        parseClickInit = false;
                    }
                    else
                    {
                        if (parseClickInit == true)
                        {
                            if (curLine.Contains("CS.UIEventListener.Get("))
                            {
                                string curInit = curLine.Substring(0, curLine.IndexOf(").onClick")).Replace("CS.UIEventListener.Get(", "").Trim();
                                for (int j = 0; j < clickInitList.Count; j++)
                                {
                                    if (clickInitList[j].Replace(" ", "").Contains("(" + curInit + ")"))
                                    {
                                        clickInitList.Remove(clickInitList[j]);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    allText += curLine;
                    allText += "\n";
                    curLine = reader.ReadLine();

                }
                if (hasClickFunction == false)
                {
                    if (functionList.Count > 0)
                    {
                        string text1 = allText.Substring(0, allText.LastIndexOf("end"));
                        string text2 = allText.Substring(allText.LastIndexOf("end"), allText.Length - allText.LastIndexOf("end"));
                        allText = text1;
                        allText += "----------------工具生成点击事件 start -------------------";
                        for (int i = 0; i < functionList.Count; i++)
                        {
                            allText += "function " + functionList[i] + " ()";
                            allText += "\n";
                            allText += "end";
                            allText += "\n";
                            Debug.Log("lua新增工具生成点击事件： " + functionList[i]);
                        }
                        allText += "----------------工具生成点击事件 end -------------------";
                        allText += "\n";
                        allText += text2;
                    }
                }
            }
            else
            {
                hasError = true;
                Debug.LogError("暂未支持不是使用工具生成的lua脚本！！");
                return;
            }
            reader.Close();
            if (!hasError)
            {
                if (functionList.Count > 0 || clickInitList.Count > 0 || variableInitList.Count > 0 || variableList.Count > 0)
                {
                    if (File.Exists(fileName))
                    {
                        string[] str = allText.Split('\n');
                        File.WriteAllLines(fileName, str, Encoding.UTF8);
                        Debug.Log("lua新增变量和方法成功写入" + scriptName + ".txt！！！！");
                        //SetUIToLuaBehaviour();
                        AssetDatabase.Refresh();
                    }
                }
                else
                {
                    Debug.Log("未发现有新增组件！！！！！！！！");
                    return;
                }

            }
        }
        catch (IOException e)
        {
            Debug.LogError("读取lua脚本文件出错：" + e);
        }
    }

    [MenuItem("UITools/UI自动生成工具/Lua用 设置UI组件挂到LuaBehaviour")]
    public static void SetUIToLuaBehaviour()
    {
        Transform parentTf = Selection.activeGameObject.transform;
        string scriptName = GetFirstCharUp(parentTf.name) + "Lua.lua";
        string directoryPath = Application.dataPath + "/HotFix/LuaScript/";
        string fileName = directoryPath + parentTf.name + "/" + scriptName + ".txt";
        if (File.Exists(fileName) == false)
        {
            fileName = EditorUtility.OpenFilePanel("选择要设置的lua脚本", directoryPath, "txt");
        }
        Debug.Log("设置lua脚本" + fileName);
        FileInfo file = new FileInfo(fileName);
        if (file == null)
        {
            Debug.LogError("当前选中对象不存在对应的脚本，请检查！！！");
            return;
        }
        List<string> variableList = new List<string>();
        try
        {
            StreamReader readerOld = file.OpenText();
            string allLine = readerOld.ReadToEnd();
            readerOld.Close();
            StreamReader reader = file.OpenText();
            bool isVariable = false;
            string curLine = reader.ReadLine();
            while (curLine != null)
            {
                if (string.IsNullOrEmpty(curLine.Trim()) == true)
                {
                    curLine = reader.ReadLine();
                    continue;
                }
                if (curLine.Contains("工具生成变量 start"))
                {
                    isVariable = true;
                }
                else if (curLine.Contains("工具生成变量 end"))
                {
                    isVariable = false;
                    break;
                }
                else
                {
                    if (isVariable)
                    {
                        string curVariable = curLine.Substring(0, curLine.IndexOf("=")).Replace("local", "").Trim();
                        if (variableList.Contains(curVariable) == false && curVariable != "luaGlobal")
                        {
                            variableList.Add(curVariable);
                        }
                    }
                }
                curLine = reader.ReadLine();
            }
            reader.Close();
        }
        catch (IOException e)
        {
            Debug.LogError("读取lua脚本文件出错：" + e);
        }
        Transform[] tfs = parentTf.GetComponentsInChildren<Transform>(true);
        LuaBehaviour lua = parentTf.GetComponent<LuaBehaviour>();
        if (lua == null)
        {
            lua = parentTf.gameObject.AddComponent<LuaBehaviour>();
        }
        string textPath = fileName.Substring(fileName.IndexOf("Assets"), fileName.Length - fileName.IndexOf("Assets"));
        Debug.Log(textPath);
        TextAsset text = AssetDatabase.LoadAssetAtPath<TextAsset>(textPath);
        if (text != null)
        {
            lua.luaAsset = text;
            lua.chunkName = scriptName.Replace(".lua", "");
            List<StringToGameObject> luaList = new List<StringToGameObject>();
            List<string> luaNameList = new List<string>();
            for (int i = 0; i < tfs.Length; i++)
            {
                if (tfs[i].name.EndsWith("|lua"))
                {
                    if (luaNameList.Contains(tfs[i].name))
                    {
                        Debug.LogError("存在同名的lua组件，请检查！！！！====>" + tfs[i].name);
                        return;
                    }
                    string luaVar = GetFirstCharDown(tfs[i].name).Replace("|", "_").Replace("_lua", "");
                    if (variableList.Contains(luaVar) == false)
                    {
                        Debug.LogError(scriptName + ".txt" + "中不存在变量： " + luaVar);
                        continue;
                    }
                    else
                    {
                        variableList.Remove(luaVar);
                    }
                    luaNameList.Add(tfs[i].name);
                    string gameObjectName = GetFirstCharDown(tfs[i].name).Replace("|", "_");
                    StringToGameObject sToG = new StringToGameObject();
                    sToG.name = gameObjectName;
                    sToG.gameObject = tfs[i].gameObject;
                    luaList.Add(sToG);
                }
            }
            if (variableList.Count > 0)
            {
                for (int i = 0; i < variableList.Count; i++)
                {
                    if (variableList[i].Contains("_pref"))
                    {
                        StringToGameObject sToG = new StringToGameObject();
                        string gameObjectName = variableList[i] + "_lua";
                        sToG.name = gameObjectName;
                        bool find = false;
                        List<GameObject> objs = FindPathGameObj("Assets/ResourcesAsset/UI");
                        //下面就可以对该资源做任何你想要的操作了，如查找已丢失的脚本、检查赋值命名等，这里查找所有的Text组件个数
                        for (int j = 0; j < objs.Count; j++)
                        {
                            if (GetFirstCharDown(objs[j].name) == GetPrefName(gameObjectName.Replace("_lua", "")))
                            {
                                sToG.gameObject = objs[j];
                                Debug.Log("lua成功设置预制体：" + gameObjectName.Replace("_lua", ""));
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            List<GameObject> objs1 = FindPathGameObj("Assets/ResourcesAsset");
                            for (int j = 0; j < objs1.Count; j++)
                            {
                                if (GetFirstCharDown(objs1[j].name) == GetPrefName(gameObjectName.Replace("_lua", "")))
                                {
                                    sToG.gameObject = objs1[j];
                                    Debug.Log("lua成功设置预制体：" + gameObjectName.Replace("_lua", ""));
                                    find = true;
                                    break;
                                }
                            }
                        }
                        if (!find)
                        {
                            List<GameObject> objs2 = FindPathGameObj("Assets/Resources");
                            for (int j = 0; j < objs2.Count; j++)
                            {
                                if (GetFirstCharDown(objs2[j].name) == GetPrefName(gameObjectName.Replace("_lua", "")))
                                {
                                    sToG.gameObject = objs2[j];
                                    Debug.Log("lua成功设置预制体：" + gameObjectName.Replace("_lua", ""));
                                    find = true;
                                    break;
                                }
                            }
                        }
                        if (!find)
                        {
                            Debug.LogError("lua未找到名为" + GetPrefName(gameObjectName.Replace("_lua", "")) + "的预制体，请检查！！！！");
                            return;
                        }
                        else
                        {
                            luaList.Add(sToG);
                        }
                    }
                    else if (variableList[i] == "luaBehaviour")
                    {
                        continue;
                    }
                    else
                    {
                        Debug.LogError("未找到" + scriptName + ".txt" + "中的变量" + variableList[i] + "对应的组件，请检查！！！！");
                        return;
                    }
                }
            }
            lua.gameObjects = luaList;
        }
        else
        {
            Debug.LogError("ssetDatabase.LoadAssetAtPath读取lua脚本失败============>" + fileName);
        }
    }

    private static string GetLuaVariableInit(string varName)
    {
        string typeName = GetTransType(varName);
        string defineName = varName.Replace("|", "_");
        switch (typeName)
        {
            case "go":
                {
                    return defineName + " = " + defineName + "_lua";
                }
            case "tf":
                {
                    return defineName + " = " + defineName + "_lua.transform";
                }
            case "lbl":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UILabel))";
                }
            case "spr":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UISprite))";
                }
            case "tex":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UITexture))";
                }
            case "grid":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UIGrid))";
                }
            case "sv":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UIScrollView))";
                }
            case "sbar":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UIScrollBar))";
                }
            case "tb":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UITable))";
                }
            case "panel":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UIPanel))";
                }
            case "sd":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UISlider))";
                }
            case "btn":
                {
                    return defineName + " = " + defineName + "_lua";
                }
            case "pref":
                {
                    return defineName + " = " + defineName + "_lua";
                }
            case "ip":
                {
                    return defineName + " = " + defineName + "_lua:GetComponent(typeof(CS.UIInput))";
                }
            default:
                Debug.LogError("lua无法生成" + varName + "的定义!!!");
                return "";
        }
    }
#endregion

#endif
}

