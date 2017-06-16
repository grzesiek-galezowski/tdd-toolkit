using System;
using System.Collections.Generic;

namespace TypeResolution.TypeResolution.CustomCollections
{
  public class ArrayElementPicking
  {
    private readonly Dictionary<Type, object> _cachesPerType = new Dictionary<Type, object>();

    public LatestArraysWithPossibleValues<T> For<T>()
    {
      if (!_cachesPerType.ContainsKey(typeof (T)))
      {
        _cachesPerType[typeof (T)] = new LatestArraysWithPossibleValues<T>();
      }
      var result = _cachesPerType[typeof (T)];
      return (LatestArraysWithPossibleValues<T>) result;
    }
  }
}