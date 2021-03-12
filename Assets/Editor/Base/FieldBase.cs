using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class FieldBase : AbstractField
{
    private string m_FieldName = "";
    private string m_FieldType = "";
    private string m_FieldDefaultValue = "";
    private string m_AccessModifier = "";
    private string m_DeclarationModifier = "";

    public FieldBase(string access, string declaration, string name, string type, string defaultValue)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_FieldName = name;
        m_FieldType = type;
        m_FieldDefaultValue = defaultValue;
    }

    protected override string GetAccessModifier()
    {
        return m_AccessModifier;
    }

    protected override string GetDeclarationModifier()
    {
        return m_DeclarationModifier;
    }

    protected override string GetFieldDefaultValue()
    {
        return m_FieldDefaultValue;
    }

    protected override string GetFieldName()
    {
        return m_FieldName;
    }

    protected override string GetFieldType()
    {
        return m_FieldType;
    }

    public override string GetValue()
    {
        StringBuilder builder = new StringBuilder();

        string format = "{0} ";
        var access = GetAccessModifier();
        if (string.IsNullOrEmpty(access) == false)
        {
            builder.AppendFormat(format, access);
        }

        var delaration = GetDeclarationModifier();
        if (string.IsNullOrEmpty(delaration) == false)
        {
            builder.AppendFormat(format, delaration);
        }

        builder.AppendFormat(format, GetFieldType());

        builder.Append(GetFieldName());

        var defaultValue = GetFieldDefaultValue();
        if (string.IsNullOrEmpty(defaultValue) == false)
        {
            builder.AppendFormat(" = {0}", defaultValue);
        }
        builder.Append(";");
        return builder.ToString();
    }
}
