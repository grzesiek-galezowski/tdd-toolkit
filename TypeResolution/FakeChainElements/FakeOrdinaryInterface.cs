using Castle.DynamicProxy;
using TddEbook.TddToolkit.TypeResolution.Interceptors;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public class FakeOrdinaryInterface<T> : IResolution<T>
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly ProxyGenerator _proxyGenerator;

    public FakeOrdinaryInterface(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      _cachedGeneration = cachedGeneration;
      _proxyGenerator = proxyGenerator;
    }

    public bool Applies()
    {
      return typeof (T).IsInterface;
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return (T)_proxyGenerator.CreateInterfaceProxyWithoutTarget(
        typeof(T), new InterfaceInterceptor(_cachedGeneration, instanceGenerator.Instance));
    }
  }
}