using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T>
  {
    T Resolve(IInstanceGenerator instanceGenerator);
  }

  public class FakeChain<T> : IFakeChain<T>
  {
    private readonly IChainElement<T> _chainHead;
    //bug pass CollectionGenerator here, but make non-static


    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve(IInstanceGenerator instanceGenerator)
    {
      return _chainHead.Resolve(instanceGenerator);
    }
  }
}