using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T>
  {
    T Resolve(IProxyBasedGenerator proxyBasedGenerator);
  }

  public class FakeChain<T> : IFakeChain<T>
  {
    private readonly IChainElement<T> _chainHead;
    //bug pass CollectionGenerator here, but make non-static


    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve(IProxyBasedGenerator proxyBasedGenerator)
    {
      return _chainHead.Resolve(proxyBasedGenerator);
    }
  }
}