using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator<T> : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;

    public UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(instance1, null), 
        "a == null should return false", violations);
      RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(null, instance1), 
        "null == a should return false", violations);

      T null1 = null;
      T null2 = null;
      RecordedAssertions.True(null1 == null2, "null == null should be true", violations);
    }
  }
}