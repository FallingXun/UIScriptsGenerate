using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Inspector设置抽象类
/// </summary>
public abstract class AbstractInspector
{
    protected abstract List<Type> GetHandleType();

    protected abstract Dictionary<string, object> ParseFieldsData(GameObject go);

    protected abstract void SetFieldsValue(object mono, Dictionary<string, object> data);

    //protected abstract void SetSpecialSetting();
}
