using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(IInstanceGenerator instanceGenerator)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}