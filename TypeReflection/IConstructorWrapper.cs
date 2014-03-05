using System;
using System.Collections.Generic;

namespace TddEbook.TypeReflection
{
  public interface IConstructorWrapper
  {
    bool HasNonPointerArgumentsOnly();
    bool HasLessParametersThan(int numberOfParams);
    int GetParametersCount();
    bool HasAbstractOrInterfaceArguments();
    List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator);
    bool IsParameterless();
  }
}
