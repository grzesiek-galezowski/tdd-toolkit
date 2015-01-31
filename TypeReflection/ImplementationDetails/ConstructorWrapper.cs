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
    private readonly ParameterInfo[] _parameters;
    private readonly bool _hasAbstractOrInterfaceArguments;

    public ConstructorWrapper(ConstructorInfo constructor)
    {
      _constructor = constructor;
      _parameters = _constructor.GetParameters();
      _hasAbstractOrInterfaceArguments =
        _parameters.Any(argument => argument.ParameterType.IsAbstract || argument.ParameterType.IsInterface);
    }

    public bool HasNonPointerArgumentsOnly()
    {
      if(!_parameters.Any(p => p.ParameterType.IsPointer))
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
      if (_parameters.Count() < numberOfParams)
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
      return _parameters.Count();
    }

    public bool HasAbstractOrInterfaceArguments()
    {
      return _hasAbstractOrInterfaceArguments;
    }

    public List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator)
    {
      var constructorValues = new List<object>();

      foreach (var constructorParam in _parameters)
      {
        constructorValues.Add(instanceGenerator(constructorParam.ParameterType));
      }
      return constructorValues;
    }

    public bool IsParameterless()
    {
      return GetParametersCount() == 0;
    }

    public string GetDescriptionForParameter(int i)
    {
      return GetDescriptionFor(_constructor.GetParameters()[i]);
    }

    public override string ToString()
    {
      var description = _constructor.DeclaringType.Name + "(";
      var parameters = _constructor.GetParameters();

      int parametersCount = GetParametersCount();
      for (int i = 0; i < parametersCount; ++i)
      {
        description += GetDescriptionFor(parameters[i]) + Separator(i, parametersCount);
      }

      description += ")";

      return description;
    }

    private static string Separator(int i, int parametersCount)
    {
      return ((i == parametersCount - 1) ? "" : ", ");
    }

    private static string GetDescriptionFor(ParameterInfo parameter)
    {
      return parameter.ParameterType.Name + " " + parameter.Name;
    }
  }
}