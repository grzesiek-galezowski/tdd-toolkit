using System;
using System.Collections.Generic;
using System.Reflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class DefaultParameterlessConstructor : IConstructorWrapper
  {
    private readonly ConstructorInfo _constructor;

    public DefaultParameterlessConstructor(ConstructorInfo constructor)
    {
      _constructor = constructor;
    }

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

    public string GetDescriptionForParameter(int i)
    {
      return string.Empty;
    }

    public object InvokeWithParametersCreatedBy(Func<Type, object> instanceGenerator)
    {
      return _constructor.Invoke(new object[]{});
    }

    public object InvokeWith(IEnumerable<object> constructorParameters)
    {
      return _constructor.Invoke(new object[] { });
    }
  }
}