using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(IProxyBasedGenerator proxyBasedGenerator)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}