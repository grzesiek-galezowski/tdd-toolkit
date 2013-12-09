using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeSet<T> : IResolution<T>
  {
    public bool Applies()
    {
      var type = typeof(T);
      var result = type.IsGenericType &&
                   (type.GetGenericTypeDefinition() == typeof (ISet<>)
                    || type.GetGenericTypeDefinition() == typeof (HashSet<>));
      return result;
    }

    public T Apply()
    {
      var type = typeof(T);
      var type1 = type.GetGenericArguments()[0];
      return (T)Any.Set(type1);
    }
  }
}