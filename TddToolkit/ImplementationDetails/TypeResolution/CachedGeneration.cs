using System;
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

    public void SetupReturnValueFor(IInvocation invocation)
    {
      var returnType = invocation.Method.ReturnType;
      if (this.AppliesTo(returnType))
      {
        invocation.ReturnValue = GetReturnTypeFor(invocation);
      }
    }

    public object GetReturnTypeFor(IInvocation invocation)
    {
      var cacheKey = ReturnValueCacheKey.For(invocation);
      if (!_cache.AlreadyContainsValueFor(cacheKey))
      {
        var returnType = invocation.Method.ReturnType;
        var returnValue = Any.Instance(returnType);
        _cache.Add(cacheKey, returnValue);
      }

      return _cache.ValueFor(cacheKey);
    }

    public bool AppliesTo(Type returnType)
    {
      return returnType != typeof (void);
    }
  }
}