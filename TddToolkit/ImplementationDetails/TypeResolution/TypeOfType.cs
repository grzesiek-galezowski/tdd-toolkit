namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class TypeOfType
  {
    public static bool Is<T>()
    {
      return typeof(T).FullName == "System.RuntimeType" || typeof (T).FullName == "System.Type";
    }
  }
}