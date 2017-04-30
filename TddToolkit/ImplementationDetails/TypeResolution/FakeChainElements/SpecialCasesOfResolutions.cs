using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public static class SpecialCasesOfResolutions<T>
  {
    public static IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(Any.KeyValuePair), typeof(KeyValuePair<,>)
        );
    }

    public static IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(Any.SortedDictionary), typeof(SortedDictionary<,>));
    }

    public static IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.SortedSet), typeof(SortedSet<>));
    }

    public static IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(Any.SortedList), typeof(SortedList<,>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(Any.Dictionary),
        typeof(IDictionary<,>), 
        typeof(IReadOnlyDictionary<,>), 
        typeof(Dictionary<,>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.Set), 
        typeof(ISet<>), typeof(HashSet<>));
    }

    public static IResolution<T> CreateResolutionOfConcurrentStack()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.ConcurrentStack),
        typeof(ConcurrentStack<>));
    }

    public static IResolution<T> CreateResolutionOfConcurrentQueue()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.ConcurrentQueue),
        typeof(ConcurrentQueue<>), typeof(IProducerConsumerCollection<>));
    }

    public static IResolution<T> CreateResolutionOfConcurrentBag()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.ConcurrentBag),
        typeof(ConcurrentBag<>));
    }

    public static IResolution<T> CreateResolutionOfConcurrentDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(Any.ConcurrentDictionary),
        typeof(ConcurrentDictionary<,>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.List),
        typeof(IList<>), 
        typeof(IEnumerable<>), 
        typeof(ICollection<>), 
        typeof(List<>),
        typeof(IReadOnlyList<>));
    }

    public static IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfArrays<T>();
    }

    public static IResolution<T> CreateResolutionOfGenericEnumerator()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(Any.Enumerator),
        typeof(IEnumerator<>)
      );
    }


  }

  public class ResolutionOfArrays<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof (T).IsArray;
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return (T)Any.Array(typeof (T).GetElementType());
    }
  }
}