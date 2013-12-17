using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public class BinaryOperator<T, TResult>
  {
    private readonly MethodInfo _method;

    public BinaryOperator(MethodInfo method)
    {
      _method = method;
    }

    public TResult Evaluate(T instance1, T instance2)
    {
      return (TResult)_method.Invoke(null, new[] { (object)instance1, (object)instance2 });
    }
  }
}