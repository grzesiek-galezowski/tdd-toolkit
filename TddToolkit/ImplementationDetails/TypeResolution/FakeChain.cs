namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeChain<T> where T : class
  {
    public static FakeChain<T> NewInstance(CachedGeneration cachedGeneration, NestingLimit nestingLimit)
    {
      return new FakeChain<T>(
        nestingLimit,
          new ChainElement<T>(
            new FakeSpecialCase<T>(),
            new ChainElement<T>(
              new FakeEnumerableOf<T>(),
              new ChainElement<T>(
                new FakeOrdinaryInterface<T>(cachedGeneration),
                new ChainElement<T>(
                  new FakeAbstractClass<T>(cachedGeneration),
                  new ChainElement<T>(
                    new FakeConcreteClassWithNonConcreteConstructor<T>(),
                    new ChainElement<T>(
                      new FakeConcreteClass<T>(),
                      new NullChainElement<T>())))))));
    }

    private readonly NestingLimit _nestingLimit;
    private readonly IChainElement<T> _chainHead;

    public FakeChain(NestingLimit nestingLimit, IChainElement<T> chainHead)
    {
      _nestingLimit = nestingLimit;
      _chainHead = chainHead;
    }

    public T Resolve()
    {
      try
      {
        _nestingLimit.AddNesting();
        if (!_nestingLimit.IsReached())
        {
          return _chainHead.Resolve();
        }
        else
        {
          return default(T);
        }
      }
      finally
      {
        _nestingLimit.RemoveNesting();
      }
    }
  }
}