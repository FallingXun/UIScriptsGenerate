using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LuaParameterBase : ParameterBase
{
    public override string GetValue()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(GetParameterName());

        return builder.ToString();
    }
}
