using System;
using System.Linq.Expressions;
using FluentAssertions;

namespace TddEbook.TddToolkit
{
    public partial class XAssert
    {
      public static void AttributeExistsOnMethodOf<T>(
        Attribute attr, Expression<Action<T>> methodExpression)
      {
        var method = Method.Of(methodExpression);
        method.HasAttribute(attr.GetType(), attr).Should().BeTrue();
      }

      public static void AttributeExistsOnMethodOf<T, TAttr>(Expression<Action<T>> methodExpression)
      {
        var method = Method.Of(methodExpression);
        method.HasAttribute<TAttr>().Should().BeTrue();
      }
    }
}
