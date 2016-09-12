using System;

namespace TddEbook.TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    bool IsNotDeveloperDefinedReadOnlyField();
    string ShouldNotBeMutableButIs();
    string GenerateExistenceMessage();
    void SetValue(object result, object instance);
    Type FieldType { get; }
  }
}
