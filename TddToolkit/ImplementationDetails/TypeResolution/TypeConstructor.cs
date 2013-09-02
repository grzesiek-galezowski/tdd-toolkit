using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class TypeConstructor
  {
    private readonly ConstructorInfo _constructor;

    public TypeConstructor(ConstructorInfo constructor)
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
      foreach (var argument in _constructor.GetParameters())
      {
        if (argument.ParameterType.IsAbstract || argument.ParameterType.IsInterface)
        {
          return true;
        }
      }
      return false;
    }

    public static IEnumerable<TypeConstructor> ExtractAllFrom(Type type)
    {
      return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                 .Select(c => new TypeConstructor(c));
    }

    public List<object> GenerateAnyParameterValues()
    {
      var constructorParams = _constructor.GetParameters();
      var constructorValues = new List<object>();

      foreach (var constructorParam in constructorParams)
      {
        constructorValues.Add(AnyReturnValue.Of(constructorParam.ParameterType).Generate());
      }
      return constructorValues;
    }
  }
}