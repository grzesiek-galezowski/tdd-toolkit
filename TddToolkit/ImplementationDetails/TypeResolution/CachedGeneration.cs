using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using NSubstitute.Core;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  [Serializable]
  public class CachedReturnValueGeneration
  {
    private readonly PerMethodCache<object> _cache;

    public CachedReturnValueGeneration(PerMethodCache<object> cache)
    {
      _cache = cache;
    }

    public void SetupReturnValueFor(IInvocation invocation)
    {
      var interceptedInvocation = new InterceptedInvocation(invocation);
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

  public class InterceptedInvocation
  {
    public readonly IInvocation _invocation;

    public InterceptedInvocation(IInvocation invocation)
    {
      _invocation = invocation;
    }

    public bool HasReturnValue()
    {
      return _invocation.Method.ReturnType != typeof (void);
    }

    public bool IsPropertySetter()
    {
      return _invocation.Method.DeclaringType.GetProperties()
        .Any(prop => prop.GetSetMethod() == _invocation.Method);
    }

    public PerMethodCacheKey GetPropertyGetterCacheKey()
    {
      var propertyFromSetterCallOrNull =
        _invocation.Method.GetPropertyFromSetterCallOrNull();
      var getter = propertyFromSetterCallOrNull.GetGetMethod(true);
      var key = PerMethodCacheKey.For(getter, _invocation.Proxy);
      return key;
    }

    public void GenerateAndAddPropertyGetterReturnValueTo(PerMethodCache<object> perMethodCache)
    {
      var key = GetPropertyGetterCacheKey();
      perMethodCache.Overwrite(key, _invocation.Arguments.First());
    }

    public void GenerateAndAddMethodReturnValueTo(PerMethodCache<object> perMethodCache)
    {
      var cacheKey = PerMethodCacheKey.For(_invocation);
      if (!perMethodCache.AlreadyContainsValueFor(cacheKey))
      {
        var returnValue = AnyInstanceOfReturnTypeOf(_invocation);
        perMethodCache.Add(cacheKey, returnValue);
        _invocation.ReturnValue = returnValue;
      }
      else
      {
        _invocation.ReturnValue = perMethodCache.ValueFor(cacheKey);
      }
    }

    public static object AnyInstanceOfReturnTypeOf(IInvocation invocation)
    {
      return Any.Instance(invocation.Method.ReturnType);
    }

  }


}