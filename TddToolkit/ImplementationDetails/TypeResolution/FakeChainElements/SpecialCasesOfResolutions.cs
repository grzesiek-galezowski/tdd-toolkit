using System.Collections.Generic;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public static class SpecialCasesOfResolutions<T>
  {
    public static IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Types(Any.KeyValuePair), typeof(KeyValuePair<,>)
        );
    }

    public static IResolution<T> CreateResolutionOfSortedDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Types(Any.SortedDictionary), typeof(SortedDictionary<,>));
    }

    public static IResolution<T> CreateResolutionOfSortedSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Type(Any.SortedSet), typeof(SortedSet<>));
    }

    public static IResolution<T> CreateResolutionOfSortedList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Types(Any.SortedList), typeof(SortedList<,>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleDictionary()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Types(Any.Dictionary), typeof(IDictionary<,>), typeof(Dictionary<,>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleSet()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Type(Any.Set), typeof(ISet<>), typeof(HashSet<>));
    }

    public static ResolutionOfTypeWithGenerics<T> CreateResolutionOfSimpleIEnumerableAndList()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Type(Any.List),
        typeof(IList<>), typeof(IEnumerable<>), typeof(ICollection<>), typeof(List<>));
    }

    public static IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Type(Any.Array),
        typeof(System.Array));
    }
  }
}