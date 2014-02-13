using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class StateBasedEqualityMustBeImplementedInTermsOfEqualsMethod<T> : IConstraint
  {
    private readonly ValueObjectActivator<T> _activator;

    public StateBasedEqualityMustBeImplementedInTermsOfEqualsMethod(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      var instance2 = _activator.CreateInstanceAsValueObjectWithPreviousParameters();

      RecordedAssertions.True(instance1.Equals(instance2), "a.Equals(b) should return true if both are created with the same arguments", violations);
      RecordedAssertions.True(instance2.Equals(instance1), "b.Equals(a) should return true if both are created with the same arguments", violations);
      RecordedAssertions.True(((object)instance1).Equals((object)instance2), "(object)a.Equals((object)b) should return true if both are created with the same arguments", violations);
      RecordedAssertions.True(((object)instance2).Equals((object)instance1), "(object)b.Equals((object)a) should return true if both are created with the same arguments", violations);
    }
  }
}