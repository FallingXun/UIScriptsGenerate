using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Const
{
    public static Dictionary<string, Type> m_ComponentDict = new Dictionary<string, Type>()
    {
        { Tag_GameObject,typeof(GameObject) },
        { Tag_Transform,typeof(Transform) },
        { Tag_RectTransform,typeof(RectTransform) },
        { Tag_TextMeshProUGUI,typeof(TextMeshProUGUI) },
        { Tag_Image,typeof(Image) },
        { Tag_RawImage,typeof(RawImage) },
        { Tag_Button,typeof(Button) },
        { Tag_Slider,typeof(Slider) },
    };

    public const string Tag_GameObject = "go";
    public const string Tag_Transform = "tf";
    public const string Tag_RectTransform = "rtf";
    public const string Tag_TextMeshProUGUI = "text";
    public const string Tag_Image = "img";
    public const string Tag_RawImage = "rimg";
    public const string Tag_Button = "btn";
    public const string Tag_Slider = "sd";

    public const string Access_Public = "public";
    public const string Access_Private = "private";
    public const string Access_Protected = "protected";
    public const string Access_Internal = "internal";

    public const string Namespace_System_Collections = "System.Collections";
    public const string Namespace_System_Collections_Generic = "System.Collections.Generic";
    public const string Namespace_UnityEngine = "UnityEngine";
    public const string Namespace_UnityEngine_UI = "UnityEngine.UI";
    public const string Namespace_TMPro = "TMPro";

    public const string Sign_Fields = "/* Auto Generated Fields Insert To Here */";
    public const string Sign_Methods = "/* Auto Generated Methods Insert To Here */";

    public const string Class_UICtrlBase = "UICtrlBase";
    public const string Class_UISubCtrlBase = "UISubCtrlBase";
    public const string Class_ScreenBase = "ScreenBase";
    public const string Class_SubScreenBase = "SubScreenBase";
    public const string Class_UIOpenScreenParameterBase = "UIOpenScreenParameterBase";


    public const string Str_UICtrlEndType = "Ctrl";
    public const string Str_UISubCtrlEndType = "SubCtrl";
    public const string Str_UIScreenEndType = "Screen";
    public const string Str_UISubScreenEndType = "SubScreen";

    public const string Symbol_Split = "|";
    public const string Symbol_Combine = "_";
}
