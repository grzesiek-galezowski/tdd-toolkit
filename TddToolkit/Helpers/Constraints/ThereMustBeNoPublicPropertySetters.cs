using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class ThereMustBeNoPublicPropertySetters<T> : IConstraint
  {

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var properties = TypeWrapper<T>.GetAllInstanceProperties();

      foreach (var item in properties.Where(item => item.HasPublicSetter()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}