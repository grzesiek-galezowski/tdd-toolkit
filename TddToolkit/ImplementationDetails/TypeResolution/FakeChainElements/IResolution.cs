namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply();
  }
}