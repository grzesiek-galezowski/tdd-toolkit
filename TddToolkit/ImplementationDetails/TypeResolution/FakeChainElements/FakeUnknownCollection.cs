using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddEbook.TypeReflection;
using Type = System.Type;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeUnknownCollection<T> : IResolution<T>
  {
    public bool Applies()
    {
      var isCollection = TypeOf<T>.IsImplementationOfOpenGeneric(typeof (IProducerConsumerCollection<>))
               || TypeOf<T>.IsImplementationOfOpenGeneric(typeof(ICollection<>));
      return TypeOf<T>.IsConcrete() &&
             typeof (T).IsGenericType &&
             isCollection &&
             TypeOf<T>.HasParameterlessConstructor();

    }


    public T Apply()
    {
      var collectionType = typeof (T);
      var collectionInstance = Activator.CreateInstance(collectionType);
      var elementTypes = collectionType.GetGenericArguments();

      var addMethod = collectionType.GetMethod("Add", elementTypes)
        ?? collectionType.GetMethod("TryAdd", elementTypes)
        ?? collectionType.GetMethod("Push", elementTypes)
        ?? collectionType.GetMethod("Enqueue", elementTypes);

      addMethod.Invoke(collectionInstance, AnyInstancesOf(elementTypes));
      addMethod.Invoke(collectionInstance, AnyInstancesOf(elementTypes));
      addMethod.Invoke(collectionInstance, AnyInstancesOf(elementTypes));

      return (T) collectionInstance;
    }

    private static object[] AnyInstancesOf(Type[] elementTypes)
    {
      return elementTypes.Select(t => Any.Instance(t)).ToArray();
    }
  }
}