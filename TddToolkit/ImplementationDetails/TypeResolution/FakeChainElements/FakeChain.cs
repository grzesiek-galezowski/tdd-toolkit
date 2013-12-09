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
          new ChainElement<T>(new FakeEnumerableOf<T>(),
          new ChainElement<T>(new FakeSet<T>(),
          new ChainElement<T>(new FakeDictionary<T>(),
          new ChainElement<T>(new FakeOrdinaryInterface<T>(cachedGeneration),
          new ChainElement<T>(new FakeAbstractClass<T>(cachedGeneration),
          new ChainElement<T>(new FakeConcreteClassWithNonConcreteConstructor<T>(),
          new ChainElement<T>(new FakeConcreteClass<T>(),
          new NullChainElement<T>()))))))))));
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