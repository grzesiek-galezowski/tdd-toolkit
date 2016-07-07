namespace TddEbook.TddToolkit.AlternativeSyntaxes
{
    public class Any<T>
    {
      public static T Instance()
      {
        return TddToolkit.Any.Instance<T>();
      }
    }

}
