using System;
using System.Reflection;
using System.Runtime.CompilerServices;
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
  }
}