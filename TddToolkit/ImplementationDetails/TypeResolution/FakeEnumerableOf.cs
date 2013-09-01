using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeEnumerableOf<T> : IResolution<T>
  {
    public bool Applies()
    {
      var type = typeof (T);
      var result = type.IsGenericType && 
             (type.GetGenericTypeDefinition() == typeof(IList<>) 
              || type.GetGenericTypeDefinition() == typeof(IEnumerable<>) 
              || type.GetGenericTypeDefinition() == typeof(ICollection<>));
      return result;
    }

    public T Apply()
    {
      var type = typeof(T);
      Type type1 = type.GetGenericArguments()[0];
      if (type1.IsValueType)
      {
        return (T) Any.List(type1);
      }
      else
      {
        return (T)Any.ListOfDerivativesFrom(type1);
      }
    }
  }
}