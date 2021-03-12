using System.Collections.Generic;
using UnityEditor;

public abstract class AbstractClass : Editor
{
    protected abstract List<string> GetUsingNamespace();

    protected abstract string GetAccessModifier();

    protected abstract string GetDeclarationModifier();

    protected abstract string GetClassName();

    protected abstract string GetParentClass();

    protected abstract AbstractMethod GetConstructedFunction();

    protected abstract List<AbstractField> GetClassFields();

    protected abstract List<AbstractMethod> GetClassMethods();

    public abstract string UpdateClass(string oldClass);

    public abstract string CreateClass();
   
}
