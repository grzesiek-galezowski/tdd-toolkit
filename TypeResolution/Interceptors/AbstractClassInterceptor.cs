using System;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.TypeResolution.Interceptors
{
  [Serializable]
  internal class AbstractClassInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly Func<Type, object> _instanceSource;

    public AbstractClassInterceptor(
      CachedReturnValueGeneration cachedGeneration, Func<Type, object> instanceSource)
    {
      _cachedGeneration = cachedGeneration;
      _instanceSource = instanceSource;
    }

    public void Intercept(IInvocation invocation)
    {
      if (invocation.Method.IsAbstract)
      {
        _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource);
      }
      else if (invocation.Method.IsVirtual)
      {
        try
        {
          var previousReturnValue = invocation.ReturnValue;

          invocation.Proceed();

          if (invocation.ReturnValue == previousReturnValue)
          {
            _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource);
          }
        }
        catch (Exception)
        {
          _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource);
        }
      }
    }


  }
}