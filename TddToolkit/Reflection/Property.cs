using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TddEbook.TddToolkit.CommonTypes;

namespace TddEbook.TddToolkit.Reflection
{
  public class Property
  {
    public static Property ObjectOf<T>(Expression<Func<T, object>> expression)
    {
      var propertyUsageExppression = expression.Body as MemberExpression;
      if (propertyUsageExppression != null)
      {
        var propertyInfo = propertyUsageExppression.Member as PropertyInfo;
        if (propertyInfo != null)
        {
          return new Property(propertyInfo);
        }
      }
      throw new Exception("The expression is not a property body");
    }

    public static Maybe<Property> FromUnaryExpression<T>(Expression<Func<T, object>> expression)
    {
      var unaryExpression = expression.Body as UnaryExpression;
      var propertyUsageExppression = unaryExpression.Operand as MemberExpression;
      if (propertyUsageExppression != null)
      {
        var propertyInfo = propertyUsageExppression.Member as PropertyInfo;
        if (propertyInfo != null)
        {
          return new Property(propertyInfo);
        }
      }
      return null;
    }


    public static Property ValueOf<T, U>(Expression<Func<T, U>> expression) where U : struct
    {
      var propertyUsageExppression = expression.Body as MemberExpression;
      if (propertyUsageExppression != null)
      {
        var propertyInfo = propertyUsageExppression.Member as PropertyInfo;
        if (propertyInfo != null)
        {
          return new Property(propertyInfo);
        }
      }
      throw new Exception("The expression is not a property body");
    }
    public string Name
    {
      get { return _propertyInfo.Name; }
    }

    public bool HasAttribute<T>()
    {
      return Attribute.IsDefined(_propertyInfo, typeof(T));
    }

    public bool HasAttribute<T>(T expectedAttribute) where T : class
    {
      var attrs = Attribute.GetCustomAttributes(_propertyInfo, typeof(T));
      var any = attrs.Any(
        currentAttribute => AreEqual(expectedAttribute, currentAttribute)
        );
      return any;
    }

    private static bool AreEqual<TAttribute>
      (
      TAttribute attr1, Attribute attr2
      ) where TAttribute : class
    {
      return attr2 is TAttribute
             && TddEbook.TddToolkit.Are.Alike(attr1, attr2 as TAttribute);
    }

    private Property(PropertyInfo property)
    {
      _propertyInfo = property;
    }

    private readonly PropertyInfo _propertyInfo;
  }

  public class Field
  {
    private readonly FieldInfo _fieldInfo;

    public Field(FieldInfo fieldInfo)
    {
      _fieldInfo = fieldInfo;
    }

    public string Name
    {
      get { return _fieldInfo.Name; }
    }

    public static Maybe<Field> FromUnaryExpression<T>(Expression<Func<T, object>> expression)
    {
      var unaryExpression = expression.Body as UnaryExpression;
      var propertyUsageExppression = unaryExpression.Operand as MemberExpression;
      if (propertyUsageExppression != null)
      {
        var fieldInfo = propertyUsageExppression.Member as FieldInfo;
        if (fieldInfo != null)
        {
          return new Field(fieldInfo);
        }
      }
      return null;
    }

  }
}