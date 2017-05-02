using System.Collections.Concurrent;
using System.Collections.Generic;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class SpecialCasesOfResolutions<T>
  {
    public IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics((p,t1,t2) => Any.KeyValuePair(t1,t2)), typeof(KeyValuePair<,>)
        );
    }

    public IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics((p, t1, t2) => Any.SortedDictionary(t1,t2)), typeof(SortedDictionary<,>));
    }

    public IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic((p, t1) => Any.SortedSet(t1)), typeof(SortedSet<>));
    }

    public IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics((p,t1,t2) => Any.SortedList(t1,t2)), typeof(SortedList<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          (p,t1,t2) => Any.Dictionary(t1,t2)),
        typeof(IDictionary<,>), 
        typeof(IReadOnlyDictionary<,>), 
        typeof(Dictionary<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => Any.Set(t)), 
        typeof(ISet<>), typeof(HashSet<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentStack()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => Any.ConcurrentStack(t)),
        typeof(ConcurrentStack<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentQueue()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => Any.ConcurrentQueue(t)),
        typeof(ConcurrentQueue<>), typeof(IProducerConsumerCollection<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentBag()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => Any.ConcurrentBag(t)),
        typeof(ConcurrentBag<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          (p,t1,t2) => Any.ConcurrentDictionary(t1,t2)),
        typeof(ConcurrentDictionary<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (p,t) => Any.List(t)),
        typeof(IList<>), 
        typeof(IEnumerable<>), 
        typeof(ICollection<>), 
        typeof(List<>),
        typeof(IReadOnlyList<>));
    }

    public static IResolution<T> CreateResolutionOfArray(SpecialCasesOfResolutions<T> specialCasesOfResolutions)
    {
      return new ResolutionOfArrays<T>();
    }

    public IResolution<T> CreateResolutionOfGenericEnumerator()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic((p,t) => Any.Enumerator(t)),
        typeof(IEnumerator<>)
      );
    }


  }
}