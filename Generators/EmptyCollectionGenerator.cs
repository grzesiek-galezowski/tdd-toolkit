using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture;

namespace TddEbook.TddToolkit.Generators
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
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, 
        collectionType, 
        MethodBase.GetCurrentMethod().Name);
    }
  }
}