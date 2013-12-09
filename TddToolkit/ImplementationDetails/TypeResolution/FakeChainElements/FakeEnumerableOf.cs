using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeEnumerableOf<T> : IResolution<T>
  {
    public bool Applies()
    {
      var type = typeof (T);
      var result = type.IsGenericType && 
             (type.GetGenericTypeDefinition() == typeof(IList<>) 
              || type.GetGenericTypeDefinition() == typeof(IEnumerable<>) 
              || type.GetGenericTypeDefinition() == typeof(ICollection<>)
              || type.GetGenericTypeDefinition() == typeof(List<>));
      return result;
    }

    public T Apply()
    {
      var type = typeof(T);
      Type type1 = type.GetGenericArguments()[0];
      return (T) Any.List(type1);
    }
  }
}