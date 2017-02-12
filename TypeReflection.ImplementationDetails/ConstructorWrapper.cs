using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypeReflection.Interfaces;

namespace TypeReflection.ImplementationDetails
{
  public class ConstructorWrapper : IConstructorWrapper
  {
    public static ConstructorWrapper FromConstructorInfo(ConstructorInfo constructor)
    {
      return new ConstructorWrapper(constructor, constructor.Invoke, constructor.GetParameters(), constructor.DeclaringType);
    }

    public static ConstructorWrapper FromStaticMethodInfo(MethodInfo m)
    {
      return new ConstructorWrapper(m, args => m.Invoke(null, args), m.GetParameters(), m.ReturnType);
    }


    private readonly MethodBase _constructor;
    private readonly ParameterInfo[] _parameters;
    private readonly Type _returnType;
    private readonly bool _hasAbstractOrInterfaceArguments;
    private readonly Func<object[], object> _invocation;
    private readonly IEnumerable<TypeInfo> _parameterTypes;

    public ConstructorWrapper(
      MethodBase constructor, 
      Func<object[], object> invocation, 
      ParameterInfo[] parameters, 
      Type returnType)
    {
      _constructor = constructor;
      _parameters = parameters;
      _returnType = returnType;
      _parameterTypes = _parameters.Select(p => p.ParameterType.GetTypeInfo());
      _hasAbstractOrInterfaceArguments =
        _parameterTypes.Any(type => type.IsAbstract || type.IsInterface);
      _invocation = invocation;
    }

    public bool HasNonPointerArgumentsOnly()
    {
      if(!_parameterTypes.Any(type => type.IsPointer))
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

      foreach (var constructorParam in _parameterTypes)
      {
        constructorValues.Add(instanceGenerator(constructorParam));
      }
      return constructorValues;
    }

    public bool IsParameterless()
    {
      return GetParametersCount() == 0;
    }

    public string GetDescriptionForParameter(int i)
    {
      return GetDescriptionFor(_parameters[i]);
    }

    public object InvokeWithParametersCreatedBy(Func<Type, object> instanceGenerator)
    {
      return _invocation(this.GenerateAnyParameterValues(instanceGenerator).ToArray());
    }

    public object InvokeWith(IEnumerable<object> constructorParameters)
    {
      return _invocation(constructorParameters.ToArray());
    }

    public override string ToString()
    {
      var description = _constructor.DeclaringType.Name + "(";

      int parametersCount = GetParametersCount();
      for (int i = 0; i < parametersCount; ++i)
      {
        description += GetDescriptionFor(_parameters[i]) + Separator(i, parametersCount);
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

    public bool HasAnyArgumentOfType(Type type)
    {
      return _parameters.Any(p => p.ParameterType == type);
    }

    public bool IsInternal()
    {
      return IsInternal(_constructor);
    }

    public static bool IsInternal(MethodBase c)
    {
      return c.IsAssembly && !c.IsPublic && !c.IsStatic;
    }

    public bool IsFactoryMethod()
    {
      return _constructor.DeclaringType == _returnType;
    }

    public bool IsNotRecursive()
    {
      return !HasAnyArgumentOfType(_returnType);
    }

    public bool IsRecursive()
    {
      return !IsNotRecursive();
    }
  }
}