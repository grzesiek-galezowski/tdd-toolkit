namespace TddEbook.TypeReflection.Interfaces
{
  public interface IBinaryOperator<in T, out TResult>
  {
    TResult Evaluate(T instance1, T instance2);
  }

  public interface IBinaryOperator
  {
    object Evaluate(object instance1, object instance2);
  }

}
