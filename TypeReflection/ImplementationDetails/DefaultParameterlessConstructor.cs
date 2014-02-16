using System;
using System.Collections.Generic;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class DefaultParameterlessConstructor : IConstructorWrapper
  {
    public bool HasNonPointerArgumentsOnly()
    {
      return true;
    }

    public bool HasLessParametersThan(int numberOfParams)
    {
      return true;
    }

    public int GetParametersCount()
    {
      return 0;
    }

    public bool HasAbstractOrInterfaceArguments()
    {
      return false;
    }

    public List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator)
    {
      return new List<object>();
    }

    public bool IsParameterless()
    {
      return true;
    }
  }
}