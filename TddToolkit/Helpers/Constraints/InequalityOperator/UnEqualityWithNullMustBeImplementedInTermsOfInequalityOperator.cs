using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator : IConstraint
  {
    private readonly ValueObjectActivator _activator;

    public UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator(
      ValueObjectActivator activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.DoesNotThrow(() =>
        RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, null), 
          "a != null should return true", violations),
          "a != null should return true", violations);
      RecordedAssertions.DoesNotThrow(() =>
        RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, null, instance1), 
          "null != a should return true", violations),
          "null != a should return true", violations);
      
      object null1 = null;
      object null2 = null;

      RecordedAssertions.DoesNotThrow(() =>
        RecordedAssertions.False(null1 != null2, 
          "null != null should be false", violations),
          "null != null should be false", violations);
    }
  }
}