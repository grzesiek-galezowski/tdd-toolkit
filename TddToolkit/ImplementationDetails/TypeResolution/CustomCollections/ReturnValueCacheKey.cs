using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections
{
  public class PerMethodCacheKey : IEquatable<PerMethodCacheKey>
  {
    private readonly MethodInfo _method;
    private readonly object _proxy;

    private PerMethodCacheKey(MethodInfo method, object proxy)
    {
      _method = method;
      _proxy = proxy;
    }

    public static PerMethodCacheKey For(MethodInfo method, object proxy)
    {
      return new PerMethodCacheKey(method, proxy);
    }

    public static PerMethodCacheKey For(IInvocation invocation)
    {
      return new PerMethodCacheKey(invocation.Method, invocation.Proxy);
    }
    public bool Equals(PerMethodCacheKey other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(_method, other._method) && Equals(_proxy, other._proxy);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PerMethodCacheKey) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((_method != null ? _method.GetHashCode() : 0)*397) ^ (_proxy != null ? _proxy.GetHashCode() : 0);
      }
    }

    public static bool operator ==(PerMethodCacheKey left, PerMethodCacheKey right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(PerMethodCacheKey left, PerMethodCacheKey right)
    {
      return !Equals(left, right);
    }


  }
}