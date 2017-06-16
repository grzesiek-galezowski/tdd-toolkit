using System.Linq;
using TddEbook.TypeReflection;
using Type = System.Type;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  public class ResolutionOfTypeWithGenerics<T> : IResolution<T>
  {
    private readonly Type[] _matchingTypes;
    private readonly FactoryForInstancesOfGenericTypes _factoryForInstancesOfGenericTypes;

    public ResolutionOfTypeWithGenerics(FactoryForInstancesOfGenericTypes factoryForInstancesOfGenericTypes, params Type[] matchingTypes)
    {
      _factoryForInstancesOfGenericTypes = factoryForInstancesOfGenericTypes;
      _matchingTypes = matchingTypes;
    }

    public bool Applies()
    {
      var type = typeof(T);
      var result = type.IsGenericType;
      result = result && _matchingTypes.Any(matchingType => matchingType == type.GetGenericTypeDefinition());
      return result;
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      var type = typeof(T);
      return (T)_factoryForInstancesOfGenericTypes.NewInstanceOf(type, instanceGenerator);
    }

  }
}
