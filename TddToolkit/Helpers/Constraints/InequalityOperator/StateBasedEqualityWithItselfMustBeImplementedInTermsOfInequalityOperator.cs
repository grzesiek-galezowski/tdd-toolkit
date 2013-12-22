using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityWithItselfMustBeImplementedInTermsOfInequalityOperator<T>
    : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;

    public StateBasedEqualityWithItselfMustBeImplementedInTermsOfInequalityOperator(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.False(Are.NotEqualInTermsOfInEqualityOperator(instance1, instance1), 
        "a != a should return false", violations);
    }
  }
}