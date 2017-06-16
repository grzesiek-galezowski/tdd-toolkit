using TddEbook.TypeReflection;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  internal class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(IInstanceGenerator instanceGenerator)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}