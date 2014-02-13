using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class AllFieldsMustBeReadOnly<T> : IConstraint
  {
    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var fields = TypeWrapper<T>.GetAllInstanceFields();
      foreach (var item in fields.Where(item => item.IsNotDeveloperDefinedReadOnlyField()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}