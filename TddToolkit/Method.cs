using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TddEbook.TddToolkit
{
  public class Method
  {
    public static Method Of<T>(Expression<Action<T>> expression)
    {
      return new Method((expression.Body as MethodCallExpression).Method);
    }

    public bool HasAttribute<T>()
    {
      return Attribute.IsDefined(_methodInfo, typeof(T));
    }

    public bool HasAttribute<T>(T expectedAttribute) where T : Attribute
    {
      var attrs = Attribute.GetCustomAttributes(_methodInfo, typeof(T));
      var any = attrs.Any(
        currentAttribute => Are.Alike(expectedAttribute, currentAttribute)
        );
      return any;
    }

    public bool HasAttribute(Type attributeType, Attribute expectedAttribute)
    {
      var attrs = Attribute.GetCustomAttributes(_methodInfo, attributeType);
      var any = attrs.Any(
        currentAttribute => Are.Alike(expectedAttribute, currentAttribute)
        );
      return any;
    }

    public string Name { get { return _methodInfo.Name; } }

    private Method(MethodInfo method)
    {
      _methodInfo = method;
    }

    private readonly MethodInfo _methodInfo;
  }
}