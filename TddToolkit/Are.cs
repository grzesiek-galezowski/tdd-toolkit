using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

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
      return Operators<T>.Equality().Evaluate(instance1, instance2);
    }

    public static bool NotEqualInTermsOfInEqualityOperator<T>(T instance1, T instance2) where T : class
    {
      return Operators<T>.Inequality().Evaluate(instance1, instance2);
    }
  }
}
