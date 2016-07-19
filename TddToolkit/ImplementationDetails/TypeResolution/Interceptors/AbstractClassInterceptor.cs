using System;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  [Serializable]
  internal class AbstractClassInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;

    public AbstractClassInterceptor(CachedReturnValueGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      if (invocation.Method.IsAbstract)
      {
        _cachedGeneration.SetupReturnValueFor(invocation);
      }
      else if (invocation.Method.IsVirtual)
      {
        try
        {
          var previousReturnValue = invocation.ReturnValue;

          invocation.Proceed();

          if (invocation.ReturnValue == previousReturnValue)
          {
            _cachedGeneration.SetupReturnValueFor(invocation);
          }
        }
        catch (Exception)
        {
          _cachedGeneration.SetupReturnValueFor(invocation);
        }
      }
    }


  }
}