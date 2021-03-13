using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorBase : AbstractInspector
{

    protected override List<Type> GetHandleType()
    {
        return null;
    }

    protected override Dictionary<string, object> ParseFieldsData(GameObject go)
    {
        return null;
    }

    protected override void SetFieldsValue(object mono, Dictionary<string, object> data)
    {
        if (mono == null)
        {
            Debug.LogError("缺少需要设置的对象!!!");
            return;
        }
        if (data == null)
        {
            Debug.LogError("缺少需要设置的变量数据!!!");
            return;
        }
        var curType = mono.GetType();
        System.Reflection.FieldInfo[] info = curType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        for (int i = 0; i < info.Length; i++)
        {
            //Debug.Log("当前变量名：" + info[i].Name);
            bool find = false;
            object obj = null;
            if (data.TryGetValue(info[i].Name, out obj))
            {
                if (obj != null)
                {
                    info[i].SetValue(mono, obj);
                    Debug.Log("成功设置：" + info[i].Name);
                    find = true;
                }
            }
            if (find == false)
            {
                Debug.LogErrorFormat("找不到变量 {0} 的对应对象，请检查是否需要删除该变量!!!", info[i].Name);
            }
        }
    }

    public void Execute(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        var handleTypeList = GetHandleType();
        if (handleTypeList == null || handleTypeList.Count <= 0)
        {
            return;
        }
        object mono = null;
        for (int i = 0; i < handleTypeList.Count; i++)
        {
            mono = go.GetComponent(handleTypeList[i]);
            if (mono != null)
            {
                break;
            }
        }

        if (mono == null)
        {
            Debug.LogError("找不到可处理的组件，请检查是否已挂载对应组件！！！");
            return;
        }
        var data = ParseFieldsData(go);
        SetFieldsValue(mono, data);

    }
}
