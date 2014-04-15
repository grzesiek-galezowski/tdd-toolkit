using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod : IConstraint
  {
    private readonly ValueObjectActivator _activator;

    public StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod(ValueObjectActivator activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {

      RecordedAssertions.DoesNotThrow(() =>
      {
        var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
        RecordedAssertions.DoesNotThrow(() =>
          RecordedAssertions.True(instance1.Equals(instance1),
            "a.Equals(a) should return true", violations),
            "a.Equals(a) should return true", violations);
      }, "Should be able to create an object of type " + _activator.TargetType, violations);
    }
  }
}