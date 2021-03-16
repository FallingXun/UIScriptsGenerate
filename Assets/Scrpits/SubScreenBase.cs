using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScreenBase 
{
    public UISubCtrlBase CtrlBase;

    public SubScreenBase(UISubCtrlBase ctrlBase)
    {

    }

    protected virtual void Init() { }

    public virtual void Dispose() { }
}
