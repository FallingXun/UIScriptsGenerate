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

    // UI资源加载完成回调，UI关闭前再调用OpenUI不会再调用
    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        m_Ctrl = CtrlBase as TestScreenCtrl;
        
        // 注册UI监听
        RegisterUI();
        // 注册事件监听
        RegisterFevent ();
    }

    // UI初始化，每次调用OpenUI都会执行
    protected override void OnInit()
    {
        base.OnInit();
    }

    // UI销毁，可做事件注销
    public override void Dispose()
    {
        base.Dispose();
    }

    // UI关闭
    public override void OnClose()
    {
        base.OnClose();
    }

    // UI点击背景遮罩事件，默认执行OnClose方法关闭UI
    public override void OnClickMaskArea()
    {
        base.OnClickMaskArea();
    }

    // UI事件注册
    private void RegisterUI()
    {
    
    }

    // 消息事件注册
    private void RegisterFevent ()
    {
    
    }

    /* 方法自动生成位置，请勿删除此行 */

}
