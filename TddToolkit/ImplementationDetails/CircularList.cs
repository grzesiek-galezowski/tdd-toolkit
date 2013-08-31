namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class CircularList<T>
  {
    private readonly T[] _items;
    private int _currentIndex = 0;

    public CircularList(params T[] items)
    {
      this._items = items;
    }

    public T Next()
    {
      if(_currentIndex > _items.Length - 1)
      {
        _currentIndex = 0; 
      }
      var result = _items[_currentIndex];
      _currentIndex++;
      return result;
    }
  }
}

