using System.Reflection;
using NSubstitute.Core;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public static class BinaryOperator
  {
    private const string OpEquality = "op_Equality";
    private const string OpInequality = "op_Inequality";
  
    private static Maybe<MethodInfo> EqualityMethod<T>()
    {
      var equality = typeof (T).GetMethod(OpEquality);

      return equality == null? Maybe<MethodInfo>.Nothing() : new Maybe<MethodInfo>(equality);
    }

    private static Maybe<MethodInfo> InequalityMethodOf<T>()
    {
      var inequality = typeof(T).GetMethod(OpInequality);

      return inequality == null ? Maybe<MethodInfo>.Nothing() : new Maybe<MethodInfo>(inequality);
    }

    public static BinaryOperator<T, bool> EqualityFrom<T>()
    {
      return BinaryOperator<T, bool>.Wrap(EqualityMethod<T>(), "operator ==");
    }

    public static BinaryOperator<T, bool> InequalityFrom<T>()
    {
      return BinaryOperator<T, bool>.Wrap(InequalityMethodOf<T>(), "operator !=");
    }

  }
}