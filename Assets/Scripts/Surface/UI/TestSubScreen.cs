using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>/// /// 功能：/// /// 作者：/// /// </summary>
public class TestSubScreen : SubScreenBase
{
    private TestSubScreenSubCtrl m_Ctrl;
    /* 变量自动生成位置，请勿删除此行 */

    public TestSubScreen(UISubCtrlBase CtrlBase) : base(CtrlBase)
    {
    
    }

    protected override void Init()
    {
        base.Init();
        m_Ctrl = CtrlBase as TestSubScreenSubCtrl;
        
        // 注册UI监听
        RegisterUI();
        // 注册事件监听
        RegisterFevent();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    // UI事件注册
    private void RegisterUI()
    {
    
    }

    // 消息事件注册
    private void RegisterFevent()
    {
    
    }

    /* 方法自动生成位置，请勿删除此行 */

}
