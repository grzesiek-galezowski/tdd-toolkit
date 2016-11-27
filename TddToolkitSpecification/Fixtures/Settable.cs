namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public abstract class Settable<T>
  {
    private T _value;

    public T Value
    {
      set { _value = value; }
      private get { return _value; }
    }
  }
}