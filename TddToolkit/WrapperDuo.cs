using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

    public WrapperDuo<T> DoNotOverride(Expression<Action> method)
    {
      if (method.Body is MethodCallExpression)
      {
        _wrappingInterceptor.DoNotOverride(((MethodCallExpression)method.Body).Method);
      }
      //TODO else if(method.Body is PropertyExpression
    }

    public static WrapperDuo<T> With(T original, T wrapped, WrappingInterceptor wrappingInterceptor)
    {
      return new WrapperDuo<T>(original, wrapped, wrappingInterceptor);
    }
  }
}
