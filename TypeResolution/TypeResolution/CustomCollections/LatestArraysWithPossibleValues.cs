using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeResolution.TypeResolution.CustomCollections
{
  public class LatestArraysWithPossibleValues<T>
  {
    private readonly List<ArrayWithIndex<T>> _arrays = new List<ArrayWithIndex<T>>();
    private const int CacheSize = 20;
    private readonly Random _random = new Random();

    public bool Contain(IEnumerable<T> possibleValues)
    {
      return _arrays.Any(array => array.IsEquivalentTo(possibleValues.ToArray()));
    }

    public void Add(IEnumerable<T> possibleValues)
    {
      if (_arrays.Count > CacheSize)
      {
        _arrays.RemoveAt(0);
      }
      _arrays.Add(new ArrayWithIndex<T>(
        possibleValues.ToArray(), 
        _random.Next(0, possibleValues.Count())));
    }

    public T PickNextElementFrom(T[] possibleValues)
    {
      foreach (var array in _arrays)
      {
        if (array.IsEquivalentTo(possibleValues))
        {
          return array.GetNextElement();
        }
      }
      throw new Exception("Coud not pick next element in an array. Please submit an issue to fix this");
    }
  }
}