using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class AllFieldsMustBeReadOnly<T> : IConstraint
  {
    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var fields = TypeOf<T>.GetAllInstanceFields();
      foreach (var item in fields.Where(item => item.IsNotDeveloperDefinedReadOnlyField()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}