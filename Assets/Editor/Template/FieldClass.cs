using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldClass : AbstractFieldBase
{
    private string m_FieldName = "";
    private string m_FieldType = "";
    private string m_FieldDefaultValue = "";
    private string m_AccessModifier = "";
    private string m_DeclarationModifier = "";

    public FieldClass(string access, string declaration, string name, string type, string defaultValue)
    {
        m_AccessModifier = access;
        m_DeclarationModifier = declaration;
        m_FieldName = name;
        m_FieldType = type;
        m_FieldDefaultValue = defaultValue;
    }

    public override string GetAccessModifier()
    {
        return m_AccessModifier;
    }

    public override string GetDeclarationModifier()
    {
        return m_DeclarationModifier;
    }

    public override string GetFieldDefaultValue()
    {
        return m_FieldDefaultValue;
    }

    public override string GetFieldName()
    {
        return m_FieldName;
    }

    public override string GetFieldType()
    {
        return m_FieldType;
    }
}
