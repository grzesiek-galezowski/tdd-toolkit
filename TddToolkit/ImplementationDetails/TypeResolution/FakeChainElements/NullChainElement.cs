namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class NullChainElement<T> : IChainElement<T>
  {
    public T Resolve()
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}