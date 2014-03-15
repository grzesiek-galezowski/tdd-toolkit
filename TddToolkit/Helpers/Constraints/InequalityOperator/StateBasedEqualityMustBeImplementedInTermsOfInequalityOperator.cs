using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator : IConstraint
  {
    private readonly ValueObjectActivator _activator;

    public StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator(ValueObjectActivator activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      var instance2 = _activator.CreateInstanceAsValueObjectWithPreviousParameters();

      RecordedAssertions.False(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, instance2), 
        "a != b should return false if both are created with the same arguments", violations);
      RecordedAssertions.False(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance2, instance1), 
        "b != a should return false if both are created with the same arguments", violations);
    }
  }
}