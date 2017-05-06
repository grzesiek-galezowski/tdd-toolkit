using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class SpecialCasesOfResolutions<T>
  {
    private readonly GenericMethodProxyCalls _methodProxyCalls;
    private readonly CollectionGenerator _collectionGenerator;

    public SpecialCasesOfResolutions(
      GenericMethodProxyCalls methodProxyCalls, 
      CollectionGenerator collectionGenerator)
    {
      _methodProxyCalls = methodProxyCalls;
      _collectionGenerator = collectionGenerator;
    }

    public IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          AnyKeyValuePair), typeof(KeyValuePair<,>)
        );
    }

    public IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          SortedDictionary), 
        typeof(SortedDictionary<,>));
    }

    public IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p, t1) => _collectionGenerator.SortedSet(t1, p)), typeof(SortedSet<>));
    }


    public IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          (p,t1,t2) => _collectionGenerator.SortedList(t1,t2,p)), typeof(SortedList<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(AnyDictionary),
        typeof(IDictionary<,>), 
        typeof(IReadOnlyDictionary<,>), 
        typeof(Dictionary<,>));
    }

    public object AnyDictionary(IProxyBasedGenerator generator, Type keyType, Type valueType)
    {
      return _methodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {generator});
    }

    public Dictionary<TKey, TValue> AnyDictionary<TKey, TValue>(IProxyBasedGenerator generator)
    {
      return AnyDictionary<TKey, TValue>(generator, AllGenerator.Many);
    }

    public Dictionary<TKey, TValue> AnyDictionary<TKey, TValue>(IProxyBasedGenerator generator, int length)
    {
      //bug change to Any.Instance<Dictionary<Key,Value>>()
      var dict = new Dictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(generator.Instance<TKey>(), generator.Instance<TValue>());
      }
      return dict;
    }



    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => _collectionGenerator.Set(t,p)), 
        typeof(ISet<>), typeof(HashSet<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentStack()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => _collectionGenerator.ConcurrentStack(t,p)),
        typeof(ConcurrentStack<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentQueue()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => _collectionGenerator.ConcurrentQueue(t,p)),
        typeof(ConcurrentQueue<>), typeof(IProducerConsumerCollection<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentBag()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => _collectionGenerator.ConcurrentBag(t,p)),
        typeof(ConcurrentBag<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          (p,t1,t2) => _collectionGenerator.ConcurrentDictionary(t1,t2,p)),
        typeof(ConcurrentDictionary<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => _collectionGenerator.List(t,p)),
        typeof(IList<>), 
        typeof(IEnumerable<>), 
        typeof(ICollection<>), 
        typeof(List<>),
        typeof(IReadOnlyList<>));
    }

    public IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfArrays<T>();
    }

    public IResolution<T> CreateResolutionOfGenericEnumerator()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic((p,t) => _collectionGenerator.Enumerator(t,p)),
        typeof(IEnumerator<>)
      );
    }

    private object AnyKeyValuePair(IProxyBasedGenerator generator, Type keyType, Type valueType)
    {
      return Activator.CreateInstance(
        typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType), generator.Instance(keyType), generator.Instance(valueType)
      );
    }

    public object SortedDictionary(IProxyBasedGenerator generator, Type keyType, Type valueType)
    {
      return _methodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {generator});
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(IProxyBasedGenerator generator, int length)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(generator.Instance<TKey>(), generator.Instance<TValue>());
      }
      return dict;
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(IProxyBasedGenerator generator)
    {
      return SortedDictionary<TKey, TValue>(generator, AllGenerator.Many);
    }

    public ICollection<T> AddManyTo(IProxyBasedGenerator generator, ICollection<T> collection, int many)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(generator.Instance<T>());
      }
      return collection;
    }

  }
}