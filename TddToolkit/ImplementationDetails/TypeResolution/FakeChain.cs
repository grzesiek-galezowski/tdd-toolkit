namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeChain<T> where T : class
  {
    public static FakeChain<T> NewInstance(CachedGeneration cachedGeneration)
    {
      return new FakeChain<T>(
        new ChainElement<T>(new FakeType<T>(),
          new ChainElement<T>(new FakeEnumerableOf<T>(),
            new ChainElement<T>(new FakeOrdinaryInterface<T>(cachedGeneration),
              new ChainElement<T>(new FakeAbstractClass<T>(),
                new ChainElement<T>(new FakeConcreteClass<T>(), 
                  new NullChainElement<T>())
              )
            )
          )
        )
      );
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