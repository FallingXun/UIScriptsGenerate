
/// <summary>
/// 变量抽象类
/// </summary>
public abstract class AbstractParameter 
{
    protected abstract string GetParameterModifier();

    protected abstract string GetParameterType();

    protected abstract string GetParameterName();

    public abstract string GetValue();
}
