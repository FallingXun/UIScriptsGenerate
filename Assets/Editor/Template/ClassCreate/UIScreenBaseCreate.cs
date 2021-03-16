using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

public class UIScreenBaseCreate : ClassBase
{
    public UIScreenBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        if (root.name.EndsWith(Const.Str_UISubScreenEndType))
        {
            Debug.LogErrorFormat("命名以 {0} 结尾的物体请使用 {1} 生成！", Const.Str_UIScreenEndType, Const.Str_UISubScreenEndType);
            return;
        }
        if (root.name.EndsWith(Const.Str_UIScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", Const.Str_UIScreenEndType);
            return;
        }
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
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

        // 类名
        SetClassName(root.name);

        string ctrlName = root.name + Const.Str_UICtrlEndType;

        // 构造函数
        List<AbstractParameter> paramList = new List<AbstractParameter>();
        ParameterBase param = new ParameterBase("", Const.Str_UIParam, Const.Class_UIOpenScreenParameterBase);
        paramList.Add(param);

        List<string> parentParamValueList = new List<string>();
        parentParamValueList.Add(string.Format("{0}.UI{1}", typeof(UIConst).Name, ClassName));
        parentParamValueList.Add(Const.Str_UIParam);

        MethodBase construct = new MethodBase();
        construct.SetAccess(Const.Access_Public)
                .SetReturnType(ClassName)
                .SetParamList(paramList)
                .SetParentParamValueList(parentParamValueList);
        SetConstructFunc(construct);

        // 变量
        FieldBase field = new FieldBase(Const.Access_Private, "", Const.Str_UICtrlParam, ctrlName, "");
        AddField(field);

        // 重写方法
        string space = "    ";
        List<string> loadSuccessBody = new List<string>();
        loadSuccessBody.Add(string.Format(Const.Str_MethodBase, Const.Str_UIMethod_OnLoadSuccess));
        loadSuccessBody.Add(string.Format("{0}{1} = {2} as {3};", space, Const.Str_UICtrlParam, Const.Str_UICtrlBaseParam, ctrlName));
        loadSuccessBody.Add(space);
        loadSuccessBody.Add(space + "// 注册UI监听");
        loadSuccessBody.Add(string.Format("{0}{1}();", space, Const.Str_UIMethod_RegisterUI));
        loadSuccessBody.Add(space + "// 注册事件监听");
        loadSuccessBody.Add(string.Format("{0}{1}();", space, Const.Str_UIMethod_RegisterFevent));
        MethodBase loadSuccess = new MethodBase();
        loadSuccess.SetAccess(Const.Access_Protected)
                    .SetDeclaration(Const.Declaration_Override)
                    .SetMethodName(Const.Str_UIMethod_OnLoadSuccess)
                    .SetReturnType(Const.Return_Void)
                    .SetMethodBody(loadSuccessBody)
                    .SetAnnotation("UI资源加载完成回调，UI关闭前再调用OpenUI不会再调用");
        AddMethod(loadSuccess);

        List<string> onInitBody = new List<string>();
        onInitBody.Add(string.Format(Const.Str_MethodBase, Const.Str_UIMethod_OnInit));
        MethodBase onInit = new MethodBase();
        onInit.SetAccess(Const.Access_Protected)
                .SetDeclaration(Const.Declaration_Override)
                .SetMethodName(Const.Str_UIMethod_OnInit)
                .SetReturnType(Const.Return_Void)
                .SetMethodBody(onInitBody)
                .SetAnnotation("UI初始化，每次调用OpenUI都会执行");
        AddMethod(onInit);

        List<string> disposeBody = new List<string>();
        disposeBody.Add(string.Format(Const.Str_MethodBase, Const.Str_UIMethod_Dispose));
        MethodBase dispose = new MethodBase();
        dispose.SetAccess(Const.Access_Public)
                .SetDeclaration(Const.Declaration_Override)
                .SetMethodName(Const.Str_UIMethod_Dispose)
                .SetReturnType(Const.Return_Void)
                .SetMethodBody(disposeBody)
                .SetAnnotation("UI销毁，可做事件注销");
        AddMethod(dispose);

        List<string> onCloseBody = new List<string>();
        onCloseBody.Add(space + "// 先执行自身Close逻辑，再执行base.Close");
        onCloseBody.Add(space );
        onCloseBody.Add(string.Format(Const.Str_MethodBase, Const.Str_UIMethod_OnClose));
        MethodBase onClose = new MethodBase();
        onClose.SetAccess(Const.Access_Public)
                .SetDeclaration(Const.Declaration_Override)
                .SetMethodName(Const.Str_UIMethod_OnClose)
                .SetReturnType(Const.Return_Void)
                .SetMethodBody(onCloseBody)
                .SetAnnotation("UI关闭");
        AddMethod(onClose);

        List<string> onClickMaskAreaBody = new List<string>();
        onClickMaskAreaBody.Add(string.Format(Const.Str_MethodBase, Const.Str_UIMethod_OnClickMaskArea));
        MethodBase onClickMaskArea = new MethodBase();
        onClickMaskArea.SetAccess(Const.Access_Public)
                .SetDeclaration(Const.Declaration_Override)
                .SetMethodName(Const.Str_UIMethod_OnClickMaskArea)
                .SetReturnType(Const.Return_Void)
                .SetMethodBody(onClickMaskAreaBody)
                .SetAnnotation("UI点击背景遮罩事件，默认执行OnClose方法关闭UI");
        AddMethod(onClickMaskArea);

        MethodBase registerUI = new MethodBase();
        registerUI.SetAccess(Const.Access_Private)
                .SetMethodName(Const.Str_UIMethod_RegisterUI)
                .SetReturnType(Const.Return_Void)
                .SetAnnotation("UI事件注册");
        AddMethod(registerUI);

        MethodBase registerFevent = new MethodBase();
        registerFevent.SetAccess(Const.Access_Private)
                .SetMethodName(Const.Str_UIMethod_RegisterFevent)
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
        return Const.Class_ScreenBase;
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
