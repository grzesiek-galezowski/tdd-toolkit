using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;

namespace TddEbook.TddToolkit.ImplementationDetails.Spying
{
  internal class SpyingInterceptor<T> : IInterceptor
  {
    private InterfaceInterceptor _interfaceInterceptor;
    private CachedHookGeneration _cachedHookGeneration;

    public SpyingInterceptor(InterfaceInterceptor interfaceInterceptor, CachedHookGeneration cachedHookGeneration)
    {
      _interfaceInterceptor = interfaceInterceptor;
      _cachedHookGeneration = cachedHookGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      if (
        invocation.Method.DeclaringType.IsGenericType &&
        invocation.Method.DeclaringType.GetGenericTypeDefinition() == typeof(ISpyable<>) &&
        invocation.Method.Name.Equals(Method.Of<ISpyable<T>>(s => s.When(null, null)).Name)
        )
      {
        var given = invocation.Proxy;
        var when = (MethodCallExpression)(((Expression<Action<T>>)invocation.Arguments[0]).Body);
        var then = (Action<object[]>)invocation.Arguments[1];
        _cachedHookGeneration.SetupHookFor(given, when.Method, then);
      }
      else
      {
        var hook = _cachedHookGeneration.GetHookFor(invocation);
        hook.Invoke(invocation.Arguments);
        _interfaceInterceptor.Intercept(invocation);
      }
    }
  }
}
