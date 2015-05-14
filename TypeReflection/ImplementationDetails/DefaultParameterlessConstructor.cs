using System;
using System.Collections.Generic;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class DefaultParameterlessConstructor : IConstructorWrapper
  {
    private static readonly IConstructorWrapper _instance = new DefaultParameterlessConstructor();

    public static IConstructorWrapper Instance
    {
      get { return _instance; }
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
      return new object(); //bug this should be corrected!
    }

    public object InvokeWith(IEnumerable<object> constructorParameters)
    {
      return new object(); //bug this should be corrected!
    }
  }
}