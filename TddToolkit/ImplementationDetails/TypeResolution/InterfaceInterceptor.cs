using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
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
      var returnType = invocation.Method.ReturnType;
      if (_cachedGeneration.AppliesTo(returnType))
      {
        invocation.ReturnValue = _cachedGeneration.GetReturnTypeFor(invocation);
      }
    }
  }
}