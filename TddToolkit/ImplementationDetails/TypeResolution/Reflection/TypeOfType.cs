namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection
{
  public static class TypeOfType
  {
    public static bool Is<T>()
    {
      return typeof(T).FullName == "System.RuntimeType" || typeof (T).FullName == "System.Type";
    }
  }
}