using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeOrdinaryInterface<T> : IResolution<T> where T : class
  {
    private readonly CachedGeneration _cachedGeneration;
    private InterfaceInterceptor _interceptor;

    public FakeOrdinaryInterface(CachedGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
      _interceptor = new InterfaceInterceptor(_cachedGeneration);
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply()
    {
      return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(_interceptor);
    }
  }
}