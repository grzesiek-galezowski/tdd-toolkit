using System.Collections.Concurrent;
using System.Collections.Generic;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class SpecialCasesOfResolutions<T>
  {
    private readonly CollectionGenerator _collectionGenerator;

    public SpecialCasesOfResolutions(CollectionGenerator collectionGenerator)
    {
      _collectionGenerator = collectionGenerator;
    }

    public IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          _collectionGenerator.KeyValuePair), typeof(KeyValuePair<,>)
        );
    }

    public IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          _collectionGenerator.SortedDictionary), 
        typeof(SortedDictionary<,>));
    }

    public IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.SortedSet), typeof(SortedSet<>));
    }


    public IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          _collectionGenerator.SortedList), typeof(SortedList<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(_collectionGenerator.Dictionary),
        typeof(IDictionary<,>), 
        typeof(IReadOnlyDictionary<,>), 
        typeof(Dictionary<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.Set), 
        typeof(ISet<>), typeof(HashSet<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentStack()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.ConcurrentStack),
        typeof(ConcurrentStack<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentQueue()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.ConcurrentQueue),
        typeof(ConcurrentQueue<>), typeof(IProducerConsumerCollection<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentBag()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.ConcurrentBag),
        typeof(ConcurrentBag<>));
    }

    public IResolution<T> CreateResolutionOfConcurrentDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(_collectionGenerator.ConcurrentDictionary),
        typeof(ConcurrentDictionary<,>));
    }

    public ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          _collectionGenerator.List),
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
        new FactoryForInstancesOfGenericTypesWith1Generic(_collectionGenerator.Enumerator),
        typeof(IEnumerator<>)
      );
    }

  }
}