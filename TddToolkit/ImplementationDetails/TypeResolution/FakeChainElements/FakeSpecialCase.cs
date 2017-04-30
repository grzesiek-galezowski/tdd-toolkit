using System.Reflection;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeSpecialCase<T> : IResolution<T>
  {
    public bool Applies()
    {
      return 
        TypeOfType.Is<T>() || 
        typeof(T) == typeof(MethodInfo);
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return Any.ValueOf<T>();
    }
  }
}