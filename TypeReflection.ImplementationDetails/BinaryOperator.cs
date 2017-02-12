using System.Reflection;
using CommonTypes;
using TypeReflection.Interfaces;
using TypeReflection.Interfaces.Exceptions;

namespace TypeReflection.ImplementationDetails
{

  public class BinaryOperator<T, TResult> : IBinaryOperator<T,TResult>
  {
    private readonly IBinaryOperator _method;

    private BinaryOperator(IBinaryOperator binaryOperator)
    {
      _method = binaryOperator;
    }

    public TResult Evaluate(T instance1, T instance2)
    {
      return (TResult)_method.Evaluate(instance1, instance2);
    }

    public static IBinaryOperator<T, bool> Wrap(IBinaryOperator binaryOperator)
    {
      return new BinaryOperator<T, bool>(binaryOperator);
    }
  }

  public class BinaryOperator : IBinaryOperator
  {
    private readonly MethodInfo _method;

    public BinaryOperator(MethodInfo method)
    {
      _method = method;
    }

    public object Evaluate(object instance1, object instance2)
    {
      return _method.Invoke(null, new[] { instance1, instance2 });
    }

    public static IBinaryOperator Wrap(
      Maybe<MethodInfo> maybeOperator, 
      Maybe<MethodInfo> maybeFallbackOperator, 
      string op)
    {
      if (maybeOperator.Otherwise(maybeFallbackOperator).HasValue)
      {
        return new BinaryOperator(maybeOperator.Otherwise(maybeFallbackOperator).Value);
      }
      else
      {
        throw new NoSuchOperatorInTypeException("No method " + op);
      }
    }
  }
}