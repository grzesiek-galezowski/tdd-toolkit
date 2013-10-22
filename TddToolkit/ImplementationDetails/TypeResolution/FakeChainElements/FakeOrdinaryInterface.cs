using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeOrdinaryInterface<T> : IResolution<T> where T : class
  {
    private readonly CachedGeneration _cachedGeneration;

    public FakeOrdinaryInterface(CachedGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply()
    {
      return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(
        new InterfaceInterceptor(_cachedGeneration));
    }
  }
}