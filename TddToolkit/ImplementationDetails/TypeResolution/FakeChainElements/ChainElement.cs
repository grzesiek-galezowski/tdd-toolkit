using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface IChainElement<out T>
  {
    T Resolve(IProxyBasedGenerator proxyBasedGenerator);
  }

  class ChainElement<T> : IChainElement<T>
  {
    private readonly IChainElement<T> _next;
    private readonly IResolution<T> _resolution;

    public ChainElement(IResolution<T> resolution, IChainElement<T> next)
    {
      _next = next;
      _resolution = resolution;
    }

    public T Resolve(IProxyBasedGenerator proxyBasedGenerator)
    {
      if (_resolution.Applies())
      {
        return _resolution.Apply(proxyBasedGenerator);
      }
      else
      {
        return _next.Resolve(proxyBasedGenerator);
      }
    }
  }
}