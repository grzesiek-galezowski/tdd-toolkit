using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeUnknownCollection<T> : IResolution<T>
  {
    public bool Applies()
    {
      return
        typeof (T).IsGenericType &&
        IsImplementationOfOpenGeneric(typeof(ICollection<>))
        && HasParameterlessConstructor(typeof(T));
    }

    private bool HasParameterlessConstructor(Type type)
    {
      var constructors = TypeConstructor.ExtractAllFrom(type);
      return constructors.Any(c => c.IsParameterless());
    }

    private static bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return typeof (T).GetInterfaces().Any(
        ifaceType => ifaceType.GetGenericTypeDefinition() == openGenericType);
    }

    public T Apply()
    {
      var collectionType = typeof (T);
      var collectionInstance = Activator.CreateInstance(collectionType);
      var elementType = collectionInstance.GetType().GetGenericArguments().First();

      var addMethod = collectionInstance.GetType().GetMethod("Add", new[] { elementType });

      addMethod.Invoke(collectionInstance, new[] { Any.Instance(elementType) });
      addMethod.Invoke(collectionInstance, new[] { Any.Instance(elementType) });
      addMethod.Invoke(collectionInstance, new[] { Any.Instance(elementType) });

      return (T) collectionInstance;
    }
  }
}