using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class EmptyCollectionGenerator
  {
    private readonly Fixture _emptyCollectionGenerator;
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public EmptyCollectionGenerator(Fixture emptyCollectionGenerator, GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _emptyCollectionGenerator = emptyCollectionGenerator;
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.Create<List<T>>();
    }

    public object EmptyEnumerableOf(Type collectionType)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<Any>(
        collectionType, MethodBase.GetCurrentMethod().Name);
    }
  }
}