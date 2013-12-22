using System;
using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class ResolutionOfTypeWithGenerics<T> : IResolution<T>
  {
    private Type[] _matchingTypes;
    private GenericInstanceFactory _instanceFactory;

    public ResolutionOfTypeWithGenerics(GenericInstanceFactory instanceFactory, params Type[] matchingTypes)
    {
      _instanceFactory = instanceFactory;
      _matchingTypes = matchingTypes;
    }

    public bool Applies()
    {
      var type = typeof(T);
      var result = type.IsGenericType;
      result = result && _matchingTypes.Any(matchingType => matchingType == type.GetGenericTypeDefinition());
      return result;
    }

    public T Apply()
    {
      var type = typeof(T);
      return (T)_instanceFactory.Create(type);
    }

  }
}
