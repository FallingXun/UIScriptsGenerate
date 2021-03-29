using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBase
{
    public UICtrlBase CtrlBase;


    public ScreenBase(string UIName, UIOpenScreenParameterBase param = null)
    {

    }

    protected virtual void OnLoadSuccess()
    {

    }

    protected virtual void OnInit()
    {

    }

    public virtual void Dispose()
    {

    }

    public virtual void OnClose()
    {

    }

    public virtual void OnClickMaskArea()
    {

    }
}
