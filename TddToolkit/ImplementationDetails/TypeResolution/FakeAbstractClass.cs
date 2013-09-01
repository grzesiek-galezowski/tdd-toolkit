using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeAbstractClass<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply()
    {
      throw new NotSupportedException("Abstract classes are not yet supported");
    }
  }
}