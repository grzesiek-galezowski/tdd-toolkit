using System;
using System.Runtime.Serialization;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  [Serializable]
  public class InterfaceInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly Func<Type, object> _instanceSource;

    public InterfaceInterceptor(CachedReturnValueGeneration cachedGeneration, Func<Type, object> instanceSource)
    {
      _cachedGeneration = cachedGeneration;
      _instanceSource = instanceSource;
    }

    public void Intercept(IInvocation invocation)
    {
      _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource);
    }
  }
}