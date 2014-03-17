using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeOrdinaryInterface<T> : IResolution<T>
  {
    private readonly ProxyGenerator _proxyGenerator;
    private readonly InterfaceInterceptor _interceptor;

    public FakeOrdinaryInterface(CachedGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      _proxyGenerator = proxyGenerator;
      _interceptor = new InterfaceInterceptor(cachedGeneration);
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply()
    {
      return (T)_proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T), _interceptor);
    }
  }
}