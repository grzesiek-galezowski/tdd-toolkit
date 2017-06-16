using System;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit.TypeResolution
{
  [Serializable]
  public class CachedReturnValueGeneration
  {
    private readonly PerMethodCache<object> _cache;

    public CachedReturnValueGeneration(PerMethodCache<object> cache)
    {
      _cache = cache;
    }

    public void SetupReturnValueFor(IInvocation invocation, Func<Type, object> instanceSource)
    {
      var interceptedInvocation = new InterceptedInvocation(invocation, instanceSource);
      if (interceptedInvocation.HasReturnValue())
      {
        interceptedInvocation.GenerateAndAddMethodReturnValueTo(_cache);
      }
      else if (interceptedInvocation.IsPropertySetter())
      {
        interceptedInvocation.GenerateAndAddPropertyGetterReturnValueTo(_cache);
      }

    }

  }
}