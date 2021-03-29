using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaSubCtrlBase : UISubCtrlBase
{
    public List<LuaBindItem> m_LuaBindItems = new List<LuaBindItem>();

    public TextAsset m_LuaFile;
}
