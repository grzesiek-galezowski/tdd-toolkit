using System;
using System.Text;

namespace TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    bool IsNotDeveloperDefinedReadOnlyField();
    string ShouldNotBeMutableButIs();
    string GenerateExistenceMessage();
    void SetValue(object result, object instance);
    Type FieldType { get; }
    bool HasTheSameNameAs(IFieldWrapper otherConstant);
    bool HasName(string name);
    bool HasTheSameValueAs(IFieldWrapper otherConstant);
    bool HasValue(object name);
    void AssertNotDuplicateOf(IFieldWrapper otherConstant);
    void AddNameTo(StringBuilder builder);
  }
}
