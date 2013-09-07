using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class ThereMustBeNoPublicPropertySetters<T> : IConstraint<T>
  {

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var properties = TypeWrapper<T>.GetAllInstanceProperties();

      foreach (var item in properties)
      {
        if (item.HasPublicSetter())
        {
          violations.Add(item.ShouldNotBeMutableButIs());
        }
      }

    }
  }
}