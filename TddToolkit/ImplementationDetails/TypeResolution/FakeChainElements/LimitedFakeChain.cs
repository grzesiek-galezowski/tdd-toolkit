namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly NestingLimit _nestingLimit;
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(NestingLimit nestingLimit, IFakeChain<T> fakeChain)
    {
      _nestingLimit = nestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve()
    {
      try
      {
        _nestingLimit.AddNesting();
        if (!_nestingLimit.IsReached())
        {
          return _fakeChain.Resolve();
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