using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;

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
      return BinaryOperator.EqualityFrom<T>().Evaluate(instance1, instance2);
    }

    public static bool NotEqualInTermsOfInEqualityOperator<T>(T instance1, T instance2) where T : class
    {
      return BinaryOperator.InequalityFrom<T>().Evaluate(instance1, instance2);
    }
  }
}
