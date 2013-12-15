using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeOrdinaryInterface<T> : IResolution<T>
  {
    private readonly InterfaceInterceptor _interceptor;

    public FakeOrdinaryInterface(CachedGeneration cachedGeneration)
    {
      _interceptor = new InterfaceInterceptor(cachedGeneration);
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply()
    {
      return (T)new ProxyGenerator().CreateInterfaceProxyWithoutTarget(typeof(T), _interceptor);
    }
  }
}