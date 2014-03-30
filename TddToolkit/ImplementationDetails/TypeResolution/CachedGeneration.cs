using System;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class CachedReturnValueGeneration
  {
    private readonly PerMethodCache<object> _cache;

    public CachedReturnValueGeneration(PerMethodCache<object> cache)
    {
      _cache = cache;
    }

    public void SetupReturnValueFor(IInvocation invocation)
    {
      var returnType = invocation.Method.ReturnType;
      if (this.AppliesTo(returnType))
      {
        invocation.ReturnValue = GetReturnTypeFor(invocation);
      }
    }

    private object GetReturnTypeFor(IInvocation invocation)
    {
      var cacheKey = PerMethodCacheKey.For(invocation);
      if (!_cache.AlreadyContainsValueFor(cacheKey))
      {
        var returnType = invocation.Method.ReturnType;
        var returnValue = Any.Instance(returnType);
        _cache.Add(cacheKey, returnValue);
      }

      return _cache.ValueFor(cacheKey);
    }

    private bool AppliesTo(Type returnType)
    {
      return returnType != typeof (void);
    }
  }

  internal class CachedHookGeneration
  {
    private readonly PerMethodCache<Action<object[]>> _cache;

    public CachedHookGeneration(PerMethodCache<Action<object[]>> cache)
    {
      _cache = cache;
    }

    public void SetupHookFor(object proxy, MethodInfo methodCall, Action<object[]> hook)
    {
      _cache.Add(PerMethodCacheKey.For(methodCall, proxy), hook);
    }

    public Action<object[]> GetHookFor(IInvocation invocation)
    {
      var cacheKey = PerMethodCacheKey.For(invocation);
      if (!_cache.AlreadyContainsValueFor(cacheKey))
      {
        return new Action<object[]>(objects => 1.Equals(1));
      }

      return _cache.ValueFor(cacheKey);
    }

  }


}