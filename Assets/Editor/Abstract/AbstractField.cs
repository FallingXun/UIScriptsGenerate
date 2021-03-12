/// <summary>
/// 方法抽象类
/// </summary>
public abstract class AbstractField
{
    /// <summary>
    /// 访问修饰符
    /// </summary>
    /// <returns></returns>
    protected abstract string GetAccessModifier();

    /// <summary>
    /// 声明修饰符
    /// </summary>
    /// <returns></returns>
    protected abstract string GetDeclarationModifier();

    /// <summary>
    /// 类型
    /// </summary>
    /// <returns></returns>
    protected abstract string GetFieldType();

    /// <summary>
    /// 变量名
    /// </summary>
    /// <returns></returns>
    protected abstract string GetFieldName();

    /// <summary>
    /// 默认值
    /// </summary>
    /// <returns></returns>
    protected abstract string GetFieldDefaultValue();

    /// <summary>
    /// 文本值
    /// </summary>
    /// <returns></returns>
    public abstract string GetValue();
  
}
