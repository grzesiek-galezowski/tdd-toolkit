using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using System.Reflection;

namespace TddEbook.TddToolkit
{
    public partial class XAssert
    {
      public static void AttributeExistsOnMethodOf<T>(
        Attribute attr, Expression<Action<T>> methodExpression)
      {
        var method = Method.Of<T>(methodExpression);
        method.HasAttribute(attr.GetType(), attr).Should().BeTrue();
      }



    }

    public class Method
    {
      public static Method Of<T>(Expression<Action<T>> expression)
      {
        return new Method((expression.Body as MethodCallExpression).Method);
      }

      public bool HasAttribute<T>()
      {
        return Attribute.IsDefined(methodInfo, typeof(T));
      }

      public bool HasAttribute<T>(T expectedAttribute) where T : Attribute
      {
        var attrs = Attribute.GetCustomAttributes(methodInfo, typeof(T));
        var any = attrs.Any(
          currentAttribute => Are.Alike<Attribute>(expectedAttribute, currentAttribute)
        );
        return any;
      }

      public bool HasAttribute(Type attributeType, Attribute expectedAttribute)
      {
        var attrs = Attribute.GetCustomAttributes(methodInfo, attributeType);
        var any = attrs.Any(
          currentAttribute => Are.Alike(expectedAttribute, currentAttribute)
        );
        return any;
      }

      private Method(MethodInfo method)
      {
        methodInfo = method;
      }

      private MethodInfo methodInfo;
    }
}
