using System;
using System.Reflection;
using TddEbook.TddToolkit.ImplementationDetails;

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
      var method = Operators<T>.Equality();
      return (Boolean)method.Invoke(null, new[] { (object)instance1, (object)instance2 });
    }

    public static bool NotEqualInTermsOfInEqualityOperator<T>(T instance1, T instance2) where T : class
    {
      var method = Operators<T>.Inequality();
      return (Boolean)method.Invoke(null, new[] { (object)instance1, (object)instance2 });
    }
  }

  public class Operators<T>
  {
    public static MethodInfo Equality()
    {
      return typeof(T).GetMethod("op_Equality");
    }

    public static MethodInfo Inequality()
    {
      return typeof(T).GetMethod("op_Inequality");
    }
  }
}
