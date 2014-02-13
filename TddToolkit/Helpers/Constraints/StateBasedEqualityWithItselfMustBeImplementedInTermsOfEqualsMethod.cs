using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod<T> : IConstraint
  {
    private readonly ValueObjectActivator<T> _activator;

    public StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.True(instance1.Equals(instance1), "a.Equals(a) should return true", violations);
    }
  }
}