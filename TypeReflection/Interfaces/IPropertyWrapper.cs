using System;

namespace TddEbook.TypeReflection.Interfaces
{
  public interface IPropertyWrapper
  {
    bool HasPublicSetter();
    string ShouldNotBeMutableButIs();
    bool HasAbstractGetter();
    Type PropertyType { get; }
    void SetValue(object result, object value);
  }
}
