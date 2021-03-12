using System.Text;

/// <summary>
/// 变量抽象类
/// </summary>
public abstract class AbstractParameterBase 
{
    public abstract string GetParameterModifier();

    public abstract string GetParameterType();

    public abstract string GetParameterName();

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        var modifier = GetParameterModifier();
        if(string.IsNullOrEmpty(modifier) == false)
        {
            builder.Append(modifier);
            builder.Append(" ");
        }

        builder.Append(GetParameterType());
        builder.Append(" ");

        builder.Append(GetParameterName());

        return builder.ToString();
    }
}
