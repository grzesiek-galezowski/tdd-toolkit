using System;
namespace TddEbook.TypeReflection
{
  public interface IBinaryOperator<T, TResult>
  {
    TResult Evaluate(T instance1, T instance2);
  }
}
