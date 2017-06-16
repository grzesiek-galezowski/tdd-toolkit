using System.Reflection;
using TddEbook.TypeReflection;
using TypeResolution.Interfaces;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  internal class FakeSpecialCase<T> : IResolution<T>
  {
    private readonly IValueGenerator _valueGenerator;

    public FakeSpecialCase(IValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public bool Applies()
    {
      return 
        TypeOfType.Is<T>() || 
        typeof(T) == typeof(MethodInfo);
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return _valueGenerator.ValueOf<T>();
    }
  }
}