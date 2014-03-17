using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  internal class FakeAbstractClass<T> : IResolution<T>
  {
    private readonly CachedGeneration _generation;
    private ProxyGenerator _proxyGenerator;

    public FakeAbstractClass(CachedGeneration generation, ProxyGenerator proxyGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply()
    {
      var fallbackTypeGenerator = new FallbackTypeGenerator<T>();
      var result = (T)(_proxyGenerator.CreateClassProxy(
        typeof(T),
        fallbackTypeGenerator.GenerateConstructorParameters().ToArray(),
        new IInterceptor[] {new AbstractClassInterceptor(_generation)}));
      fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
      return result;
    }
  }
}