using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaScreenBaseCreate : LuaClassBase
{
    public LuaScreenBaseCreate(GameObject root)
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

        // 设置注释
        SetAnnotation(Const.Str_Lua_UIAnnotation);

        // 类名
        SetClassName(root.name);

        // 重写方法
        string space = "    ";
        List<string> loadSuccessBody = new List<string>();
        loadSuccessBody.Add(space + "-- 注册UI监听");
        loadSuccessBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_RegisterUI));
        loadSuccessBody.Add(space + "-- 注册事件监听");
        loadSuccessBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_RegisterFevent));
        LuaMethodBase loadSuccess = new LuaMethodBase();
        loadSuccess.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnLoadSuccess))
                    .SetReturnType(Const.Return_Void)
                    .SetMethodBody(loadSuccessBody)
                    .SetAnnotation("UI资源加载完成回调，UI关闭前再调用OpenUI不会再调用");
        AddMethod(loadSuccess);

        LuaMethodBase onInit = new LuaMethodBase();
        onInit.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnInit))
                .SetAnnotation("UI初始化，每次调用OpenUI都会执行");
        AddMethod(onInit);

        LuaMethodBase dispose = new LuaMethodBase();
        dispose.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_Dispose))
                .SetAnnotation("UI销毁，可做事件注销");
        AddMethod(dispose);

        LuaMethodBase onClose = new LuaMethodBase();
        onClose.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnClose))
                .SetAnnotation("UI关闭");
        AddMethod(onClose);

        List<string> onClickMaskAreaBody = new List<string>();
        onClickMaskAreaBody.Add(string.Format("    self:{0}();", Const.Str_UIMethod_OnClose));
        LuaMethodBase onClickMaskArea = new LuaMethodBase();
        onClickMaskArea.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnClickMaskArea))
                .SetMethodBody(onClickMaskAreaBody)
                .SetAnnotation("UI点击背景遮罩事件，默认执行OnClose方法关闭UI");
        AddMethod(onClickMaskArea);

        LuaMethodBase registerUI = new LuaMethodBase();
        registerUI.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_RegisterUI))
                .SetAnnotation("UI事件注册");
        AddMethod(registerUI);

        LuaMethodBase registerFevent = new LuaMethodBase();
        registerFevent.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_RegisterFevent))
                .SetAnnotation("消息事件注册");
        AddMethod(registerFevent);

        SetLegal(true);
    }

    #region 基类方法
    protected override List<AbstractField> GetClassFields()
    {
        return FieldList;
    }

    protected override string GetClassName()
    {
        return ClassName;
    }

    protected override List<AbstractMethod> GetClassMethods()
    {
        return MethodList;
    }


    protected override string GetClassAnnotation()
    {
        return Annotation;
    }
    #endregion
}
