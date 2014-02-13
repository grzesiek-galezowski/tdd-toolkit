using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection
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

    public static IEnumerable<FieldWrapper> GetAllInstanceFields()
    {
      var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      return fields.Select(f => new FieldWrapper(f));
    }

    public static IEnumerable<PropertyWrapper> GetAllInstanceProperties()
    {
      var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
      return properties.Select(p => new PropertyWrapper(p));
    }

  }
}