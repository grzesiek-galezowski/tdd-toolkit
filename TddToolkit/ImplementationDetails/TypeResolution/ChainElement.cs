namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal interface IChainElement<out T>
  {
    T Resolve();
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

    public T Resolve()
    {
      if (_resolution.Applies())
      {
        return _resolution.Apply();
      }
      else
      {
        return _next.Resolve();
      }
    }
  }
}