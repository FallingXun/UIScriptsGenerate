using System.Text;
using System.Collections.Generic;

public abstract class AbstractMethod
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
    /// 返回类型
    /// </summary>
    /// <returns></returns>
    protected abstract string GetReturnType();

    /// <summary>
    /// 方法名
    /// </summary>
    /// <returns></returns>
    protected abstract string GetMethodName();

    /// <summary>
    /// 泛型类型（未实现）
    /// </summary>
    /// <returns></returns>
    protected abstract string GetGenericity();

    /// <summary>
    /// 参数列表
    /// </summary>
    /// <returns></returns>
    protected abstract List<AbstractParameter> GetMethodParameters();

    /// <summary>
    /// 父类参数值列表
    /// </summary>
    /// <returns></returns>
    protected abstract List<string> GetParentParameterValues();

    /// <summary>
    /// 泛型限定（未实现）
    /// </summary>
    /// <returns></returns>
    protected abstract string GetGenericityLimited();

    protected abstract List<string> GetMethodBody();

    public abstract string GetValue();

    public virtual string GetPrefixSpace()
    {
        return "";
    }
   
}
