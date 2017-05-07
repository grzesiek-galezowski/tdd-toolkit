using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class ResolutionOfArrays<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof (T).IsArray;
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return (T)Any.Array(typeof (T).GetElementType());
    }
  }
}