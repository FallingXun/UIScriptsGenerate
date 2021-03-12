/// <summary>
/// 参数抽象类
/// </summary>
public abstract class AbstractParameter 
{
    /// <summary>
    /// 修饰符
    /// </summary>
    /// <returns></returns>
    protected abstract string GetParameterModifier();

    /// <summary>
    /// 类型
    /// </summary>
    /// <returns></returns>
    protected abstract string GetParameterType();

    /// <summary>
    /// 参数名
    /// </summary>
    /// <returns></returns>
    protected abstract string GetParameterName();

    /// <summary>
    /// 文本值
    /// </summary>
    /// <returns></returns>
    public abstract string GetValue();
}
