using System;
namespace TddEbook.TypeReflection
{
  public interface IPropertyWrapper
  {
    bool HasPublicSetter();
    string ShouldNotBeMutableButIs();
  }
}
