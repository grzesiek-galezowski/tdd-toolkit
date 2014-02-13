using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  internal class InterfaceInterceptor : IInterceptor
  {
    private readonly CachedGeneration _cachedGeneration;

    public InterfaceInterceptor(CachedGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      _cachedGeneration.SetupReturnValueFor(invocation);
    }
  }
}