namespace TddEbook.TypeReflection.Interfaces
{
  public interface IPropertyWrapper
  {
    bool HasPublicSetter();
    string ShouldNotBeMutableButIs();
  }
}
