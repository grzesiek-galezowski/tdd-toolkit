using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  internal class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(IInstanceGenerator instanceGenerator)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}