using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestScreen : ScreenBase
{
    private TestScreenCtrl m_Ctrl;
    /* 变量自动生成位置，请勿删除此行 */

    public TestScreen(UIOpenScreenParameterBase param) : base(UIConst.UITestScreen, param)
    {
    
    }

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        m_Ctrl = CtrlBase as TestScreenCtrl;
        
        // 注册UI监听
        RegisterUI();
        // 注册事件监听
        RegisterFevent ();
    }

    protected override void OnInit()
    {
        base.OnInit();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public override void OnClickMaskArea()
    {
        base.OnClickMaskArea();
    }

    private void RegisterUI()
    {
    
    }

    private void RegisterFevent ()
    {
    
    }

    /* 方法自动生成位置，请勿删除此行 */

}
