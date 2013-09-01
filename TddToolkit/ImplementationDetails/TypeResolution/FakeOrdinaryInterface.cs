using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
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
      return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new InterfaceInterceptor(_cachedGeneration));
    }
  }

  internal class InterfaceInterceptor : IInterceptor
  {
    private readonly CachedGeneration _cachedGeneration;

    public InterfaceInterceptor(CachedGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      var returnType = invocation.Method.ReturnType;
      if (returnType != typeof (void))
      {
        invocation.ReturnValue = _cachedGeneration.GetReturnTypeFor(invocation);
      }
    }
  }
}