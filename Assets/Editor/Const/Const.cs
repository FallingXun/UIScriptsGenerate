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
        { Tag_Toggle,typeof(Toggle) },
        { Tag_DropDown,typeof(Dropdown) },
        { Tag_ScrollRect,typeof(ScrollRect) },
        { Tag_InputField,typeof(InputField) },
        { Tag_ReusableLayoutGroup,typeof(ReusableLayoutGroup) },
        { Tag_Item,typeof(MonoBehaviour) },
    };

    public const string Tag_GameObject = "go";
    public const string Tag_Transform = "tf";
    public const string Tag_RectTransform = "rtf";
    public const string Tag_TextMeshProUGUI = "text";
    public const string Tag_Image = "img";
    public const string Tag_RawImage = "rimg";
    public const string Tag_Button = "btn";
    public const string Tag_Slider = "sd";
    public const string Tag_Toggle = "tg";
    public const string Tag_DropDown = "dd";
    public const string Tag_ScrollRect = "sr";
    public const string Tag_InputField = "input";
    public const string Tag_ReusableLayoutGroup = "rlg";
    public const string Tag_Item = "item";

    public const string Access_Public = "public";
    public const string Access_Private = "private";
    public const string Access_Protected = "protected";
    public const string Access_Internal = "internal";

    public const string Declaration_Const = "const";
    public const string Declaration_Static = "static";
    public const string Declaration_Virtual = "virtual";
    public const string Declaration_Override = "override";
    public const string Declaration_Abstract = "abstract";

    public const string Return_Int = "int";
    public const string Return_Float = "float";
    public const string Return_String = "string";
    public const string Return_Void = "void";

    public const string Namespace_System_Collections = "System.Collections";
    public const string Namespace_System_Collections_Generic = "System.Collections.Generic";
    public const string Namespace_UnityEngine = "UnityEngine";
    public const string Namespace_UnityEngine_UI = "UnityEngine.UI";
    public const string Namespace_TMPro = "TMPro";
    public const string Namespace_CnfSprotoType = "CnfSprotoType";
    public const string Namespace_DG_Tweening = "DG.Tweening";

    public const string Sign_Fields = "/* 变量自动生成位置，请勿删除此行 */";
    public const string Sign_Methods = "/* 方法自动生成位置，请勿删除此行 */";
    public const string Sign_Lua_Fields = "-- 变量自动生成位置，请勿删除此行 --";
    public const string Sign_Lua_Methods = "-- 方法自动生成位置，请勿删除此行 --";

    public const string Class_UICtrlBase = "UICtrlBase";
    public const string Class_UISubCtrlBase = "UISubCtrlBase";
    public const string Class_ScreenBase = "ScreenBase";
    public const string Class_SubScreenBase = "SubScreenBase";
    public const string Class_UIOpenScreenParameterBase = "UIOpenScreenParameterBase";


    public const string Str_UICtrlEndType = "Ctrl";
    public const string Str_UISubCtrlEndType = "SubCtrl";
    public const string Str_UIScreenEndType = "Screen";
    public const string Str_UISubScreenEndType = "SubScreen";
    public const string Str_UIParam = "param";
    public const string Str_UICtrlParam = "m_Ctrl";
    public const string Str_UICtrlBaseParam = "CtrlBase";

    public const string Str_MethodBase = "    base.{0}();";
    public const string Str_NormalSpace = "    ";

    public const string Str_UIMethod_OnLoadSuccess = "OnLoadSuccess";
    public const string Str_UIMethod_OnInit = "OnInit";
    public const string Str_UIMethod_Init = "Init";
    public const string Str_UIMethod_Dispose = "Dispose";
    public const string Str_UIMethod_OnClose = "OnClose";
    public const string Str_UIMethod_OnClickMaskArea = "OnClickMaskArea"; 
    public const string Str_UIMethod_RegisterUI = "RegisterUI"; 
    public const string Str_UIMethod_RegisterFevent = "RegisterFevent";
    public const string Str_UIMethod_UnRegisterFevent = "UnRegisterFevent";
    public const string Str_UIMethod_OnSpawn = "OnSpawn";
    public const string Str_UIMethod_OnRelease = "OnRelease";


    public const string Str_UIAnnotation = "/// <summary>\r/// \r/// 功能：\r/// \r/// 作者：\r/// \r/// </summary>";
    public const string Str_Lua_UIAnnotation = "-- 功能：\r-- 作者：\r\r";


    public const string Symbol_Split = "|";
    public const string Symbol_Combine = "_";
}
