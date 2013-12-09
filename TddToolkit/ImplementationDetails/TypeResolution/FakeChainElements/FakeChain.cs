using System.Collections.Generic;
namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  interface IFakeChain<out T> where T : class
  {
    T Resolve();
  }

  internal class FakeChain<T> : IFakeChain<T> where T : class
  {
    public static IFakeChain<T> NewInstance(CachedGeneration cachedGeneration, NestingLimit nestingLimit)
    {
      return new LimitedFakeChain<T>(
        nestingLimit,
        new FakeChain<T>(
          new ChainElement<T>(new FakeSpecialCase<T>(),
          new ChainElement<T>(CreateResolutionOfArray(),
          new ChainElement<T>(CreateResolutionOfSimpleIEnumerableAndList(),
          new ChainElement<T>(CreateResolutionOfSimpleSet(),
          new ChainElement<T>(CreateResolutionOfSimpleDictionary(),
          new ChainElement<T>(CreateResolutionOfSortedList(),
          new ChainElement<T>(CreateResolutionOfSortedSet(),
          new ChainElement<T>(CreateResolutionOfSortedDictionary(),
          new ChainElement<T>(new FakeOrdinaryInterface<T>(cachedGeneration),
          new ChainElement<T>(new FakeAbstractClass<T>(cachedGeneration),
          new ChainElement<T>(new FakeConcreteClassWithNonConcreteConstructor<T>(),
          new ChainElement<T>(new FakeConcreteClass<T>(),
          new NullChainElement<T>()))))))))))))));
    }

    private static IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new GenericInstanceFactory2(Any.SortedDictionary), typeof(SortedDictionary<,>));
    }

    private static IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new GenericInstanceFactory1(Any.SortedSet), typeof(SortedSet<>));
    }

    private static IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new GenericInstanceFactory2(Any.SortedList), typeof(SortedList<,>));
    }

    private static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
                  new GenericInstanceFactory2(Any.Dictionary), typeof(IDictionary<,>), typeof(Dictionary<,>));
    }

    private static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
                  new GenericInstanceFactory1(Any.Set), typeof(ISet<>), typeof(HashSet<>));
    }

    private static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
                  new GenericInstanceFactory1(Any.List),
                  typeof(IList<>), typeof(IEnumerable<>), typeof(ICollection<>), typeof(List<>));
    }

    private static IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfTypeWithGenerics<T>(
                  new GenericInstanceFactory1(Any.Array),
                  typeof(System.Array));
    }

    private readonly IChainElement<T> _chainHead;

    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve()
    {
      return _chainHead.Resolve();
    }
  }
}