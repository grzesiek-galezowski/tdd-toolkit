using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;

namespace TddEbook.TddToolkit.Reflection
{
  public class Metadata
  {
    public static MethodInfo ForMethod<T>(Expression<Action<T>> expression)
    {
      var methodCallExpression = expression.Body as MethodCallExpression;
      methodCallExpression.Should().NotBeNull();
      return methodCallExpression.Method;
    }

    public static string GetActionName<T>(Expression<Action<T>> expression)
    {
      return Metadata.ForMethod(expression).Name;
    }

    public static bool IsAttributeDefinedForMethod<TMethodOwner, TAttribute>(
      TAttribute expectedAttribute,
      Expression<Action<TMethodOwner>> expression) where TAttribute : class
    {
      var methodInfo = Metadata.ForMethod(expression);
      var attrs = Attribute.GetCustomAttributes(methodInfo, typeof(TAttribute));

      var any = attrs.Any(
        currentAttribute => AreEqual(expectedAttribute, currentAttribute)
      );
      return any;
    }

    public static bool IsAttributeDefinedForMethod<TMethodOwner, TAttribute>(
      Expression<Action<TMethodOwner>> expression) where TAttribute : class
    {
      var methodInfo = Metadata.ForMethod(expression);
      var attrs = Attribute.GetCustomAttributes(methodInfo, typeof(TAttribute));
      return attrs.Any();
    }

    private static bool AreEqual<TAttribute>(TAttribute attribute, Attribute attr) where TAttribute : class
    {
      return attr is TAttribute && TddEbook.TddToolkit.Are.Alike(attr as TAttribute, attribute);
    }
  }
}
