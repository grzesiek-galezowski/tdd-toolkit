using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  internal class FakeAbstractClass<T> : IResolution<T>
  {
    private readonly CachedReturnValueGeneration _generation;
    private readonly ProxyGenerator _proxyGenerator;
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeAbstractClass(
      CachedReturnValueGeneration generation, 
      ProxyGenerator proxyGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
      _fallbackTypeGenerator = new FallbackTypeGenerator<T>(); //bug extract?
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      var result = (T)(_proxyGenerator.CreateClassProxy(
        typeof(T),
        _fallbackTypeGenerator.GenerateConstructorParameters(proxyBasedGenerator).ToArray(), 
        new AbstractClassInterceptor(_generation)));
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
      return result;
    }
  }
}