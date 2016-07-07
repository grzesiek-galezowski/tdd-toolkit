namespace TddEbook.TddToolkit.AlternativeSyntaxes
{
    public class AnyData
    {
      protected T Any<T>()
      {
        return TddToolkit.Any.Instance<T>();
      }
    }

}
