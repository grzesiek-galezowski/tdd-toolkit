using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator : IConstraint
  {
    private readonly ValueObjectActivator _activator;

    public UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator(ValueObjectActivator activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.DoesNotThrow(() =>
        RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(_activator.TargetType, instance1, null), 
          "a == null should return false", violations), 
        "a == null should return false", violations);

      RecordedAssertions.DoesNotThrow(() =>
        RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(_activator.TargetType, null, instance1),
          "null == a should return false", violations),
        "null == a should return false", violations);
    }
  }
}