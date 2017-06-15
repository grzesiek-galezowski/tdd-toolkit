using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class ResolutionOfArrays<T> : IResolution<T>
  {
    private readonly CollectionGenerator _collectionGenerator;

    public ResolutionOfArrays(CollectionGenerator collectionGenerator)
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