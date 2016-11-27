using System.Collections;
using System.Collections.Generic;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public class MyOwnCollection<T> : ICollection<T>
  {
    private List<T> _list = new List<T>();

    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    public void Add(T item)
    {
      _list.Add(item);
    }

    public void Clear()
    {
      _list.Clear();
    }

    public bool Contains(T item)
    {
      return _list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      _list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
      return _list.Remove(item);
    }

    public int Count
    {
      get { return _list.Count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }
  }
}