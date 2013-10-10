using System.Linq;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  class ArrayWithIndex<T>
  {
    private readonly T[] _values;
    public int Index { get; set; }

    public T[] Values
    {
      get { return _values; }
    }

    public bool IsEquivalentTo(T[] array)
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