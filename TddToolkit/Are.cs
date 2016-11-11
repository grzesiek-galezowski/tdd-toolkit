using System;
using System.Linq.Expressions;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public class Are
  {
    public static bool Alike<T>(T expected, T actual)
    {
      return ConvertExceptionToBoolean(() => XAssert.Alike(expected, actual));
    }
    public static bool NotAlike<T>(T expected, T actual)
    {
      return ConvertExceptionToBoolean(() => XAssert.NotAlike(expected, actual));
    }

    public static bool Alike<T>(T expected, T actual, params Expression<Func<T, object>>[] skippedPropertiesOrFields)
    {
      return ConvertExceptionToBoolean(() => XAssert.Alike(expected, actual, skippedPropertiesOrFields));
    }
    public static bool NotAlike<T>(T expected, T actual, params Expression<Func<T, object>>[] skippedPropertiesOrFields)
    {
      return ConvertExceptionToBoolean(() => XAssert.NotAlike(expected, actual, skippedPropertiesOrFields));
    }


    private static bool ConvertExceptionToBoolean(Action action)
    {
      try
      {
        action();
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public static bool EqualInTermsOfEqualityOperator<T>(T instance1, T instance2) where T : class
    {
      return TypeOf<T>.Equality().Evaluate(instance1, instance2);
    }

    public static bool NotEqualInTermsOfInEqualityOperator<T>(T instance1, T instance2) where T : class
    {
      return TypeOf<T>.Inequality().Evaluate(instance1, instance2);
    }

    public static bool EqualInTermsOfEqualityOperator(Type type, object instance1, object instance2)
    {
      return (bool)SmartType.For(type).Equality().Evaluate(instance1, instance2);
    }

    public static bool NotEqualInTermsOfInEqualityOperator(Type type, object instance1, object instance2)
    {
      return (bool)SmartType.For(type).Inequality().Evaluate(instance1, instance2);
    }

  }
}
