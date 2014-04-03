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

    public WrapperDuo<T> DoNotOverride(Expression<Action<T>> method)
    {
      if (method.Body is MethodCallExpression)
      {
        _wrappingInterceptor.DoNotOverride(((MethodCallExpression)method.Body).Method);
        return this;
      }
      else
      {
        throw new ArgumentException("skipping " + Maybe.Wrap(method) + " not allowed.");
      }
    }

    public WrapperDuo<T> DoNotOverride<TResult>(Expression<Func<T, TResult>> method)
    {
      if (method.Body is MethodCallExpression)
      {
        _wrappingInterceptor.DoNotOverride(((MethodCallExpression)method.Body).Method);
        return this;
      }
      else if (method.Body is MemberExpression)
      {
        var member = ((MemberExpression)method.Body).Member;
        var propertyInfo = member as PropertyInfo;
        if (propertyInfo != null)
        {
          _wrappingInterceptor.DoNotOverride(propertyInfo.GetGetMethod());
        }
        return this;
      }
      else
      {
        throw new ArgumentException("skipping " + Maybe.Wrap(method) + " not allowed.");
      }
    }


    public static WrapperDuo<T> With(T original, T wrapped, WrappingInterceptor wrappingInterceptor)
    {
      return new WrapperDuo<T>(original, wrapped, wrappingInterceptor);
    }
  }
}
