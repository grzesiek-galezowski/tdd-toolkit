namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply();
  }
}