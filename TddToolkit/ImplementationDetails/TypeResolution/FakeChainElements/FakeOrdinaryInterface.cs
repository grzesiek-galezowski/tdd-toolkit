using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeOrdinaryInterface<T> : IResolution<T>
  {
    private readonly ProxyGenerator _proxyGenerator;
    private readonly InterfaceInterceptor _interceptor;

    public FakeOrdinaryInterface(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      _proxyGenerator = proxyGenerator;
      _interceptor = new InterfaceInterceptor(cachedGeneration);
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return (T)_proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T), _interceptor);
    }
  }
}