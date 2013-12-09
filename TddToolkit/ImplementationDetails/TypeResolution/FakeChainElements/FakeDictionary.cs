using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeDictionary<T> : IResolution<T>
  {
    public bool Applies()
    {
      var type = typeof(T);
      var result = type.IsGenericType &&
                   (type.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                    || type.GetGenericTypeDefinition() == typeof(Dictionary<,>));
      return result;
    }

    public T Apply()
    {
      var type = typeof(T);
      var keyType = type.GetGenericArguments()[0];
      var valueType = type.GetGenericArguments()[1];
      return (T)Any.Dictionary(keyType, valueType);
    }
  }

}