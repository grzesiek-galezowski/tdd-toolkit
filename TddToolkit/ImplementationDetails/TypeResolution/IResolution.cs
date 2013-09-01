namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal interface IResolution<out T>
  {
    bool Applies();
    T Apply();
  }
}