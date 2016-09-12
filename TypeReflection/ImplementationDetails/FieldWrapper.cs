using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class FieldWrapper : IFieldWrapper
  {
    private readonly FieldInfo _fieldInfo;

    public FieldWrapper(FieldInfo fieldInfo)
    {
      _fieldInfo = fieldInfo;
    }

    public bool IsNotDeveloperDefinedReadOnlyField()
    {
      return !_fieldInfo.IsInitOnly && !_fieldInfo.IsDefined(typeof (CompilerGeneratedAttribute), true);
    }

    public bool IsConstant()
    {
      return _fieldInfo.IsLiteral && IsNotDeveloperDefinedReadOnlyField();
    }

    public string ShouldNotBeMutableButIs()
    {
      return "Value objects are immutable, but field "
             + _fieldInfo.Name
             + " of type " + _fieldInfo.DeclaringType + " is mutable. Make this field readonly to pass the check.";
    }

    public string GenerateExistenceMessage()
    {
      return "Type: " + _fieldInfo.DeclaringType +
             " contains static field " + _fieldInfo.Name +
             " of type " + _fieldInfo.FieldType;

    }

    public void SetValue(object result, object instance)
    {
      _fieldInfo.SetValue(result, instance);
    }

    public Type FieldType { get { return _fieldInfo.FieldType; } }
    public bool HasTheSameNameAs(IFieldWrapper otherConstant)
    {
      return otherConstant.HasName(_fieldInfo.Name);
    }

    public bool HasName(string name)
    {
      return _fieldInfo.Name == name;
    }

    public bool HasTheSameValueAs(IFieldWrapper otherConstant)
    {
      return otherConstant.HasValue(_fieldInfo.GetValue(null));
    }

    public bool HasValue(object name)
    {
      return _fieldInfo.GetValue(null).Equals(name);
    }

    public void AssertNotDuplicateOf(IFieldWrapper otherConstant)
    {
      if (!HasTheSameNameAs(otherConstant))
      {
        if (HasTheSameValueAs(otherConstant))
        {
          var builder = new StringBuilder();
          AddNameTo(builder);
          builder.Append(" is a duplicate of ");
          otherConstant.AddNameTo(builder);
          throw new DuplicateConstantException(builder.ToString());
        }
      }
    }

    public void AddNameTo(StringBuilder builder)
    {
      builder.Append(_fieldInfo.Name + " <" + _fieldInfo.GetValue(null) + ">");
    }
  }

  public class DuplicateConstantException : Exception
  {
    public DuplicateConstantException(string message) : base(message)
    {
      
    }
  }
}