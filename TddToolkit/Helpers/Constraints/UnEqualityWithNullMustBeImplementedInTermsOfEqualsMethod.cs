using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod<T> : IConstraint<T>
  {
    private readonly ValueObjectActivator<T> _activator;

    public UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.False(((object)instance1).Equals(null), "a.Equals(null) should return false", violations);
    }
  }
}