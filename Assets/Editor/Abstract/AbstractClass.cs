using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// 基本类抽象类
/// </summary>
public abstract class AbstractClass : Editor
{
    protected abstract List<string> GetUsingNamespace();

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
    /// 类名
    /// </summary>
    /// <returns></returns>
    protected abstract string GetClassName();

    /// <summary>
    /// 父类名
    /// </summary>
    /// <returns></returns>
    protected abstract string GetParentClass();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <returns></returns>
    protected abstract AbstractMethod GetConstructedFunction();

    /// <summary>
    /// 变量列表
    /// </summary>
    /// <returns></returns>
    protected abstract List<AbstractField> GetClassFields();

    /// <summary>
    /// 方法列表
    /// </summary>
    /// <returns></returns>
    protected abstract List<AbstractMethod> GetClassMethods();

    /// <summary>
    /// 注释信息
    /// </summary>
    /// <returns></returns>
    protected abstract string GetClassAnnotation();

    /// <summary>
    /// 更新类文件
    /// </summary>
    /// <param name="oldClass">当前类文件内容</param>
    /// <returns></returns>
    public abstract string UpdateClass(string oldClass);

    /// <summary>
    /// 创建类文件
    /// </summary>
    /// <returns></returns>
    public abstract string CreateClass();
   
}
