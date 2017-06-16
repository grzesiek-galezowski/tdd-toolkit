using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddEbook.TypeReflection;
using Type = System.Type;

namespace TypeResolution.TypeResolution.FakeChainElements
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


    public T Apply(IInstanceGenerator instanceGenerator)
    {
      var collectionType = typeof (T);
      var collectionInstance = Activator.CreateInstance(collectionType);
      var elementTypes = collectionType.GetGenericArguments();

      var addMethod = collectionType.GetMethod("Add", elementTypes)
        ?? collectionType.GetMethod("TryAdd", elementTypes)
        ?? collectionType.GetMethod("Push", elementTypes)
        ?? collectionType.GetMethod("Enqueue", elementTypes);

      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator));
      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator));
      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator));

      return (T) collectionInstance;
    }

    private static object[] AnyInstancesOf(IEnumerable<Type> elementTypes, IInstanceGenerator instanceGenerator)
    {
      return elementTypes.Select(instanceGenerator.Instance).ToArray();
    }
  }
}