using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeType<T> : IResolution<T>
  {
    public bool Applies()
    {
      return TypeOfType.Is<T>();
    }

    public T Apply()
    {
      return Any.ValueOf<T>();
    }
  }
}