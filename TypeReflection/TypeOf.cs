using NSubstitute.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TypeReflection.ImplementationDetails;

namespace TddEbook.TypeReflection
{
  public static class TypeOf<T>
  {
    public static bool HasParameterlessConstructor(Type type)
    {
      var constructors = ConstructorWrapper.ExtractAllFrom(type);
      return constructors.Any(c => c.IsParameterless());
    }

    public static bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return typeof(T).GetInterfaces().Any(
        ifaceType => ifaceType.IsGenericType && ifaceType.GetGenericTypeDefinition() == openGenericType);
    }

    public static bool IsConcrete()
    {
      return !typeof(T).IsAbstract && !typeof(T).IsInterface;
    }

    public static IEnumerable<IFieldWrapper> GetAllInstanceFields()
    {
      var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      return fields.Select(f => new FieldWrapper(f));
    }

    public static IEnumerable<IPropertyWrapper> GetAllInstanceProperties()
    {
      var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
      return properties.Select(p => new PropertyWrapper(p));
    }

    public static IConstructorWrapper PickConstructorWithLeastNonPointersParameters()
    {
      var type = typeof(T);
      var constructors = ConstructorWrapper.ExtractAllFrom(type);
      IConstructorWrapper leastParamsConstructor = null;
      var numberOfParams = int.MaxValue;

      foreach (var typeConstructor in constructors)
      {
        if (typeConstructor.HasNonPointerArgumentsOnly() && typeConstructor.HasLessParametersThan(numberOfParams))
        {
          leastParamsConstructor = typeConstructor;
          numberOfParams = typeConstructor.GetParametersCount();
        }
      }
      return leastParamsConstructor;
    }

    private const string OpEquality = "op_Equality";
    private const string OpInequality = "op_Inequality";

    private static Maybe<MethodInfo> EqualityMethod()
    {
      var equality = typeof(T).GetMethod(OpEquality);

      return equality == null ? Maybe<MethodInfo>.Nothing() : new Maybe<MethodInfo>(equality);
    }

    private static Maybe<MethodInfo> InequalityMethodOf()
    {
      var inequality = typeof(T).GetMethod(OpInequality);

      return inequality == null ? Maybe<MethodInfo>.Nothing() : new Maybe<MethodInfo>(inequality);
    }

    public static IBinaryOperator<T, bool> Equality()
    {
      return BinaryOperator<T, bool>.Wrap(EqualityMethod(), "operator ==");
    }

    public static IBinaryOperator<T, bool> Inequality()
    {
      return BinaryOperator<T, bool>.Wrap(InequalityMethodOf(), "operator !=");
    }

  }
}