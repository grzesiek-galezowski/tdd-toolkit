using System;
namespace TddEbook.TypeReflection
{
  public interface IBinaryOperator<in T, out TResult>
  {
    TResult Evaluate(T instance1, T instance2);
  }
}
