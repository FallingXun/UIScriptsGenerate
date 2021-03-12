using System.Text;

public abstract class AbstractFieldBase
{
    public abstract string GetAccessModifier();

    public abstract string GetDeclarationModifier();

    public abstract string GetFieldType();

    public abstract string GetFieldName();

    public abstract string GetFieldDefaultValue();

    public override string ToString()
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
        if(string.IsNullOrEmpty(defaultValue) == false)
        {
            builder.AppendFormat(" = {0}", defaultValue);
        }

        builder.Append(";");

        return builder.ToString();
    }
}
