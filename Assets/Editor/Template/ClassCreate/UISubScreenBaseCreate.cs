using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;
using ScriptsGenerate;

public class UISubScreenBaseCreate : ClassBase
{
    public UISubScreenBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        if (root.name.EndsWith(UIConstEditor.Str_UISubScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", UIConstEditor.Str_UISubScreenEndType);
            return;
        }

        // 设置注释
        SetAnnotation(Const.Str_UIAnnotation);

        // 命名空间
        AddNamespace(Const.Namespace_System_Collections);
        AddNamespace(Const.Namespace_System_Collections_Generic);
        AddNamespace(Const.Namespace_UnityEngine);
        AddNamespace(Const.Namespace_UnityEngine_UI);
        AddNamespace(Const.Namespace_TMPro);
        //AddNamespace(Const.Namespace_CnfSprotoType);
        //AddNamespace(Const.Namespace_DG_Tweening);

        string space = "    ";

        // 类名
        SetClassName(root.name);

        string ctrlName = root.name + UIConstEditor.Str_UISubCtrlEndType;

        // 构造函数
        List<AbstractParameter> paramList = new List<AbstractParameter>();
        ParameterBase param = new ParameterBase("", UIConstEditor.Str_UICtrlBaseParam, UIConstEditor.Class_UISubCtrlBase);
        paramList.Add(param);

        List<string> parentParamValueList = new List<string>();
        parentParamValueList.Add(UIConstEditor.Str_UICtrlBaseParam);


        MethodBase construct = new MethodBase();
        construct.SetAccess(Const.Access_Public)
                .SetReturnType(ClassName)
                .SetParamList(paramList)
                .SetParentParamValueList(parentParamValueList);
        SetConstructFunc(construct);

        // 变量
        FieldBase field = new FieldBase(Const.Access_Private, "", UIConstEditor.Str_UICtrlParam, ctrlName, "");
        AddField(field);

        // 重写方法
        List<string> initBody = new List<string>();
        initBody.Add(string.Format(Const.Str_MethodBase, UIConstEditor.Str_UIMethod_Init));
        initBody.Add(string.Format("{0}{1} = {2} as {3};", space, UIConstEditor.Str_UICtrlParam, UIConstEditor.Str_UICtrlBaseParam, ctrlName));
        initBody.Add(space);
        initBody.Add(space + "// 注册UI监听");
        initBody.Add(string.Format("{0}{1}();", space, UIConstEditor.Str_UIMethod_RegisterUI));
        initBody.Add(space + "// 注册事件监听");
        initBody.Add(string.Format("{0}{1}();", space, UIConstEditor.Str_UIMethod_RegisterFevent));
        MethodBase init = new MethodBase();
        init.SetAccess(Const.Access_Protected)
                    .SetDeclaration(Const.Declaration_Override)
                    .SetMethodName(UIConstEditor.Str_UIMethod_Init)
                    .SetReturnType(Const.Return_Void)
                    .SetMethodBody(initBody)
                    .SetAnnotation("初始化");
        AddMethod(init);



        List<string> disposeBody = new List<string>();
        disposeBody.Add(string.Format(Const.Str_MethodBase, UIConstEditor.Str_UIMethod_Dispose));
        MethodBase dispose = new MethodBase();
        dispose.SetAccess(Const.Access_Public)
                .SetDeclaration(Const.Declaration_Override)
                .SetMethodName(UIConstEditor.Str_UIMethod_Dispose)
                .SetReturnType(Const.Return_Void)
                .SetMethodBody(disposeBody)
                .SetAnnotation("销毁");
        AddMethod(dispose);

        MethodBase registerUI = new MethodBase();
        registerUI.SetAccess(Const.Access_Private)
                .SetMethodName(UIConstEditor.Str_UIMethod_RegisterUI)
                .SetReturnType(Const.Return_Void)
                .SetAnnotation("UI事件注册");
        AddMethod(registerUI);

        MethodBase registerFevent = new MethodBase();
        registerFevent.SetAccess(Const.Access_Private)
                .SetMethodName(UIConstEditor.Str_UIMethod_RegisterFevent)
                .SetReturnType(Const.Return_Void)
                .SetAnnotation("消息事件注册");
        AddMethod(registerFevent);

        SetLegal(true);
    }

    #region 基类方法
    protected override string GetAccessModifier()
    {
        return Const.Access_Public;

    }

    protected override List<AbstractField> GetClassFields()
    {
        return FieldList;
    }

    protected override string GetClassName()
    {
        return ClassName;
    }

    protected override AbstractMethod GetConstructedFunction()
    {
        return ConstructFunc;
    }

    protected override List<AbstractMethod> GetClassMethods()
    {
        return MethodList;
    }

    protected override string GetParentClass()
    {
        return UIConstEditor.Class_SubScreenBase;
    }

    protected override List<string> GetUsingNamespace()
    {
        return NamespaceList;
    }

    protected override string GetClassAnnotation()
    {
        return Annotation;
    }
    #endregion
}
