using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public class Operators<T>
  {
    public static MethodInfo EqualityMethod()
    {
      return typeof(T).GetMethod("op_Equality");
    }

    public static MethodInfo InequalityMethod()
    {
      return typeof(T).GetMethod("op_Inequality");
    }

    public static BinaryOperator<T, bool> Equality()
    {
      return new BinaryOperator<T, bool>(EqualityMethod());
    }

    public static BinaryOperator<T, bool> Inequality()
    {
      return new BinaryOperator<T, bool>(InequalityMethod());
    }

  }
}