using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  public class InterfaceInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;

    public InterfaceInterceptor(CachedReturnValueGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      _cachedGeneration.SetupReturnValueFor(invocation);
    }
  }
}