using TddEbook.TddToolkit.TypeResolution.Interfaces;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public class ResolutionOfArrays<T> : IResolution<T>
  {
    private readonly ICollectionGenerator _collectionGenerator;

    public ResolutionOfArrays(ICollectionGenerator collectionGenerator)
    {
      _collectionGenerator = collectionGenerator;
    }

    public bool Applies()
    {
      return typeof (T).IsArray;
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return (T)_collectionGenerator.Array(typeof (T).GetElementType(), instanceGenerator);
    }
  }
}