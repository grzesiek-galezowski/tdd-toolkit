using System;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class AllFieldsMustBeReadOnly : IConstraint
  {
    private Type _type;

    public AllFieldsMustBeReadOnly(Type type)
    {
      _type = type;
    }


    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var fields = TypeWrapper.For(_type).GetAllInstanceFields();
      foreach (var item in fields.Where(item => item.IsNotDeveloperDefinedReadOnlyField()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}