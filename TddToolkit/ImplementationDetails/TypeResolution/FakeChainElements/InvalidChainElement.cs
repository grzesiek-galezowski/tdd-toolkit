namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve()
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}