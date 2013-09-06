using TddEbook.TddToolkit.Helpers.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class AllFieldsMustBeReadOnly<T> : IConstraint<T>
  {
    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var fields = TypeWrapper<T>.GetAllInstanceFields();
      foreach (var item in fields)
      {
        if (item.IsNotDeveloperDefinedReadOnlyField())
        {
          violations.Add(item.ShouldNotBeMutableButIs());
        }
      }
    }
  }
}