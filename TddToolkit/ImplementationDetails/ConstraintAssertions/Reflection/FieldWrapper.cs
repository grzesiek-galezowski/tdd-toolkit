using System.Reflection;
using System.Runtime.CompilerServices;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection
{
  internal class FieldWrapper
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
             + " is mutable. Make this field readonly to pass the check.";
    }
  }
}