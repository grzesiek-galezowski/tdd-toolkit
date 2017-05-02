using System;
using System.Linq;
using Castle.DynamicProxy;
using NSubstitute.Core;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class InterceptedInvocation
  {
    private readonly IInvocation _invocation;
    private readonly Func<Type, object> _instanceSource;

    public InterceptedInvocation(
      IInvocation invocation,
      Func<Type, object> instanceSource)
    {
      _invocation = invocation;
      _instanceSource = instanceSource;
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

    private object AnyInstanceOfReturnTypeOf(IInvocation invocation)
    {
      return _instanceSource(invocation.Method.ReturnType);
    }

  }
}