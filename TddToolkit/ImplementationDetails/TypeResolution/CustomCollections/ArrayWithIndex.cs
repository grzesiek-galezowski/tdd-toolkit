using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections
{
  class ArrayWithIndex<T>
  {
    private readonly T[] _values;
    private int Index { get; set; }

    private IEnumerable<T> Values
    {
      get { return _values; }
    }

    public bool IsEquivalentTo(IEnumerable<T> array)
    {
      return Values.SequenceEqual(array);
    }

    public ArrayWithIndex(T[] values)
    {
      _values = values;
      Index = 0;
    }

    public T GetNextElement()
    {
      if (Index == _values.Length)
      {
        Index = 0;
      }
      var result = _values[Index];
      Index++;
      return result;
    }
  }
}