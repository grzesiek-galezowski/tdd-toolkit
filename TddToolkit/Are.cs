using System;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public class Are
  {
    public static bool Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      return comparison.Compare(expected, actual);
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
      return (bool)TypeWrapper.For(type).Equality().Evaluate(instance1, instance2);
    }

    public static bool NotEqualInTermsOfInEqualityOperator(Type type, object instance1, object instance2)
    {
      return (bool)TypeWrapper.For(type).Inequality().Evaluate(instance1, instance2);
    }

  }
}
