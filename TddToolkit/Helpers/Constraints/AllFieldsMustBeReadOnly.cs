using System;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class AllFieldsMustBeReadOnly : IConstraint
  {
    private readonly Type _type;

    public AllFieldsMustBeReadOnly(Type type)
    {
      _type = type;
    }


    public void CheckAndRecord(ConstraintsViolations violations)
    {
      CheckImmutability(violations, _type);
    }

    private void CheckImmutability(ConstraintsViolations violations, Type type)
    {
      var fields = SmartType.For(type).GetAllInstanceFields().ToList();
      var fieldWrappers = fields
        .Where(item => item.IsNotDeveloperDefinedReadOnlyField());

      foreach (var item in fieldWrappers)
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}