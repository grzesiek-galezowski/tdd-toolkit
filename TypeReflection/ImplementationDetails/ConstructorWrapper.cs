using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class ConstructorWrapper : IConstructorWrapper
  {
    private readonly ConstructorInfo _constructor;

    public ConstructorWrapper(ConstructorInfo constructor)
    {
      _constructor = constructor;
    }

    public bool HasNonPointerArgumentsOnly()
    {
      var parameters = _constructor.GetParameters();
      if(!parameters.Any(p => p.ParameterType.IsPointer))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool HasLessParametersThan(int numberOfParams)
    {
      var parameters = _constructor.GetParameters();
      if (parameters.Count() < numberOfParams)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public int GetParametersCount()
    {
      return _constructor.GetParameters().Count();
    }

    public bool HasAbstractOrInterfaceArguments()
    {
      return _constructor.GetParameters().Any(argument => argument.ParameterType.IsAbstract || argument.ParameterType.IsInterface);
    }

    public List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator)
    {
      var constructorParams = _constructor.GetParameters();
      var constructorValues = new List<object>();

      foreach (var constructorParam in constructorParams)
      {
        constructorValues.Add(instanceGenerator(constructorParam.ParameterType));
      }
      return constructorValues;
    }

    public bool IsParameterless()
    {
      return GetParametersCount() == 0;
    }
  }
}