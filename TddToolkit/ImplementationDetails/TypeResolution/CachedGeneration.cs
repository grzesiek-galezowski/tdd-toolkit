using System.Collections.Generic;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class CachedGeneration
  {
    private readonly ReturnValueCache _cache;

    public CachedGeneration(ReturnValueCache cache)
    {
      _cache = cache;
    }

    public object GetReturnTypeFor(IInvocation invocation)
    {
      var cacheKey = ReturnValueCacheKey.For(invocation);
      if (!_cache.AlreadyContainsValueFor(cacheKey))
      {
        var returnType = invocation.Method.ReturnType;
        var returnValue = AnyInstanceReturnValue.New(returnType).Generate();
        _cache.Add(cacheKey, returnValue);
      }

      return _cache.ValueFor(cacheKey);
    }
  }

  internal class ReturnValueCache
  {
    private readonly Dictionary<ReturnValueCacheKey, object> _cache = new Dictionary<ReturnValueCacheKey, object>();
    
    public bool AlreadyContainsValueFor(ReturnValueCacheKey cacheKey)
    {
      return _cache.ContainsKey(cacheKey);
    }

    public void Add(ReturnValueCacheKey cacheKey, object returnValue)
    {
      _cache.Add(cacheKey, returnValue);
    }

    public object ValueFor(ReturnValueCacheKey cacheKey)
    {
      return _cache[cacheKey];
    }
  }
}