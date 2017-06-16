using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using TddEbook.TypeReflection;

namespace TypeResolution.TypeResolution.Interceptors
{
  public class WrappingInterceptor : IInterceptor
  {
    private readonly HashSet<MethodInfo> _methodsNotToOverride = new HashSet<MethodInfo>();
    private readonly IInterceptor _interfaceInterceptor;
    private readonly HashSet<MethodInfo> _methodsWithForcedOverride = new HashSet<MethodInfo>();

    public WrappingInterceptor(IInterceptor interfaceInterceptor)
    {
      _interfaceInterceptor = interfaceInterceptor;
    }
    public void Intercept(IInvocation invocation)
    {
      invocation.Proceed();
      if (IsDefaultValueAResultOf(invocation) && !_methodsNotToOverride.Contains(invocation.Method))
      {
        _interfaceInterceptor.Intercept(invocation);
      }
      else if (_methodsWithForcedOverride.Contains(invocation.Method))
      {
        _interfaceInterceptor.Intercept(invocation);
      }
    }

    private static bool IsDefaultValueAResultOf(IInvocation invocation)
    {
      return invocation.ReturnValue == null || invocation.ReturnValue.Equals(DefaultValue.Of(invocation.Method.ReturnType));
    }

    internal void DoNotOverride(MethodInfo methodInfo)
    {
      _methodsNotToOverride.Add(methodInfo);
    }

    public void ForceOverride(MethodInfo methodInfo)
    {
      _methodsWithForcedOverride.Add(methodInfo);
    }
  }
}
