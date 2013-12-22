using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualityOperator<T>
    : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;

    public StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualityOperator(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.True(Are.EqualInTermsOfEqualityOperator(instance1, instance1), 
        "a == a should return true", violations);
    }
  }
}