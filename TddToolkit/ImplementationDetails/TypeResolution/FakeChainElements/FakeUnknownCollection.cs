using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeUnknownCollection<T> : IResolution<T>
  {
    public bool Applies()
    {
      return TypeOf<T>.IsConcrete() &&
        typeof (T).IsGenericType &&
        TypeOf<T>.IsImplementationOfOpenGeneric(typeof(ICollection<>))
        && TypeOf<T>.HasParameterlessConstructor(typeof(T));
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