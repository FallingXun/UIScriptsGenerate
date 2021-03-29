using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaSubScreenBaseCreate : LuaClassBase
{
    public LuaSubScreenBaseCreate(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        if (root.name.EndsWith(Const.Str_UISubScreenEndType) == false)
        {
            Debug.LogErrorFormat("请选择命名以 {0} 结尾的物体！", Const.Str_UISubScreenEndType);
            return;
        }
        Transform[] tfs = root.GetComponentsInChildren<Transform>(true);
        if (tfs == null || tfs.Length <= 0)
        {
            return;
        }
        string space = "    ";

        // 设置注释
        SetAnnotation(Const.Str_Lua_UIAnnotation);

        // 类名
        SetClassName(root.name);

        // 重写方法
        List<string> initBody = new List<string>();
        initBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_OnSpawn));
        LuaMethodBase init = new LuaMethodBase();
        init.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_Init))
                    .SetMethodBody(initBody)
                    .SetAnnotation("初始化");
        AddMethod(init);

        List<string> disposeBody = new List<string>();
        disposeBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_OnRelease));
        LuaMethodBase dispose = new LuaMethodBase();
        dispose.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_Dispose))
                .SetMethodBody(disposeBody)
                .SetAnnotation("销毁"); 
        AddMethod(dispose);

        List<string> onSpawnBody = new List<string>();
        onSpawnBody.Add(space + "-- 注册UI监听");
        onSpawnBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_RegisterUI));
        onSpawnBody.Add(space + "-- 注册事件监听");
        onSpawnBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_RegisterFevent));
        LuaMethodBase onSpawn = new LuaMethodBase();
        onSpawn.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnSpawn))
                .SetMethodBody(onSpawnBody)
                .SetAnnotation("提取");
        AddMethod(onSpawn);

        List<string> onReleaseBody = new List<string>();
        onReleaseBody.Add(space + "-- 移除事件监听");
        onReleaseBody.Add(string.Format("{0}self:{1}();", space, Const.Str_UIMethod_UnRegisterFevent));
        LuaMethodBase onRelease = new LuaMethodBase();
        onRelease.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_OnRelease))
                .SetMethodBody(onReleaseBody)
                .SetAnnotation("回收"); 
        AddMethod(onRelease);

        MethodBase registerUI = new LuaMethodBase();
        registerUI.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_RegisterUI))
                .SetAnnotation("UI事件注册");
        AddMethod(registerUI);

        LuaMethodBase registerFevent = new LuaMethodBase();
        registerFevent.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_RegisterFevent))
                .SetAnnotation("消息事件注册");
        AddMethod(registerFevent);

        LuaMethodBase unRegisterFevent = new LuaMethodBase();
        unRegisterFevent.SetMethodName(string.Format("{0}:{1}", ClassName, Const.Str_UIMethod_UnRegisterFevent))
                .SetAnnotation("消息事件取消注册");
        AddMethod(unRegisterFevent);

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
