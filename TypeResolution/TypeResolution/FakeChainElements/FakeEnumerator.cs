using System.Collections;
using TddEbook.TypeReflection;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  public class FakeEnumerator<T> : IResolution<T>
  {
    public FakeEnumerator()
    {
    }

    public bool Applies()
    {
      return TypeOf<T>.Is<IEnumerator>();
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return (T)(instanceGenerator.Instance<object[]>().GetEnumerator());
    }
  }
}