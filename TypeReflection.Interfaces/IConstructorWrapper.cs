using System;
using System.Collections.Generic;

namespace TypeReflection.Interfaces
{
  public interface IConstructorWrapper : AutoFixture.Kernel.IMethod
  {
    bool HasNonPointerArgumentsOnly();
    bool HasLessParametersThan(int numberOfParams);
    int GetParametersCount();
    bool HasAbstractOrInterfaceArguments();
    List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator);
    bool IsParameterless();
    string GetDescriptionForParameter(int i);
    object InvokeWithParametersCreatedBy(Func<Type, object> instanceGenerator);
    object InvokeWith(IEnumerable<object> constructorParameters);
    bool HasAnyArgumentOfType(Type type);
    bool IsInternal();
    bool IsNotRecursive();
    bool IsRecursive();
  }
}
