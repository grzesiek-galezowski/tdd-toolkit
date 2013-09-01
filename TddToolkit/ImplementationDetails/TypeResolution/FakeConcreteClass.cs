using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeConcreteClass<T> : IResolution<T>
  {
    private Type _type;

    public FakeConcreteClass()
    {
      _type = typeof (T);
    }

    public bool Applies()
    {
      return true;
    }

    public T Apply()
    {
      return Any.ValueOf<T>();
    }
  }
}