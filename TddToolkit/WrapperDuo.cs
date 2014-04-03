using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TddEbook.TypeReflection.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public class WrapperDuo<T>
  {
    private readonly WrappingInterceptor _wrappingInterceptor;
    public T Prototype { get; private set; }
    public T Object { get; private set; } 

    public WrapperDuo(T original, T wrapped, WrappingInterceptor wrappingInterceptor)
    {
      this.Prototype = original;
      this.Object = wrapped;
      _wrappingInterceptor = wrappingInterceptor;
    }

    public WrapperDuo<T> NoOverrideFor<TResult>(Expression<Func<T, TResult>> method)
    {
      if (method.Body is MethodCallExpression)
      {
        _wrappingInterceptor.DoNotOverride(ExtractMethodInfoFromMethodCallExpression(method));
        return this;
      }
      else if (method.Body is MemberExpression)
      {
        ExtractPropertyGetExpressionFrom(method, _wrappingInterceptor.DoNotOverride);
        return this;
      }
      else
      {
        throw new ArgumentException("skipping " + Maybe.Wrap(method) + " not allowed.");
      }
    }

    public WrapperDuo<T> ForceOverrideFor<TResult>(Expression<Func<T, TResult>> method)
    {
      if (method.Body is MethodCallExpression)
      {
        _wrappingInterceptor.ForceOverride(ExtractMethodInfoFromMethodCallExpression(method));
        return this;
      }
      else if (method.Body is MemberExpression)
      {
        ExtractPropertyGetExpressionFrom(method, _wrappingInterceptor.ForceOverride);
        return this;
      }
      else
      {
        throw new ArgumentException("skipping " + Maybe.Wrap(method) + " not allowed.");
      }
    }

    private static void ExtractPropertyGetExpressionFrom<TResult>(Expression<Func<T, TResult>> method, Action<MethodInfo> destinationCall)
    {
      var member = ((MemberExpression) method.Body).Member;
      var propertyInfo = member as PropertyInfo;
      if (propertyInfo != null)
      {
        destinationCall(propertyInfo.GetGetMethod());
      }
    }

    private static MethodInfo ExtractMethodInfoFromMethodCallExpression<TResult>(Expression<Func<T, TResult>> method)
    {
      return ((MethodCallExpression)method.Body).Method;
    }


    public static WrapperDuo<T> With(T original, T wrapped, WrappingInterceptor wrappingInterceptor)
    {
      return new WrapperDuo<T>(original, wrapped, wrappingInterceptor);
    }

  }
}
