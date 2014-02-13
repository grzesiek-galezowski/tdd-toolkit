using System;
using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public static class TypeOf<T>
  {
    public static bool HasParameterlessConstructor(Type type)
    {
      var constructors = TypeConstructor.ExtractAllFrom(type);
      return constructors.Any(c => c.IsParameterless());
    }

    public static bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return typeof(T).GetInterfaces().Any(
        ifaceType => ifaceType.IsGenericType && ifaceType.GetGenericTypeDefinition() == openGenericType);
    }

    public static bool IsConcrete()
    {
      return !typeof (T).IsAbstract && !typeof (T).IsInterface;
    }
  }
}