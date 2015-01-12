namespace TddEbook.TddToolkit
{
  public class Clone
  {
    public static T Of<T>(T instance)
    {
      return NClone.Clone.ObjectGraph(instance);
    }
  }
}
