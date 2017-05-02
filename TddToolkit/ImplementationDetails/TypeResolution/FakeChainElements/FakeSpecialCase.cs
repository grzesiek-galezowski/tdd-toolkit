using System.Reflection;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeSpecialCase<T> : IResolution<T>
  {
    private readonly ValueGenerator _valueGenerator;

    public FakeSpecialCase(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public bool Applies()
    {
      return 
        TypeOfType.Is<T>() || 
        typeof(T) == typeof(MethodInfo);
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return _valueGenerator.ValueOf<T>();
    }
  }
}