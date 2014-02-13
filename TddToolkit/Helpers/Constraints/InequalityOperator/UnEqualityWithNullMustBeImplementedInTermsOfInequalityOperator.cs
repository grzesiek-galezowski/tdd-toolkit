using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator<T> : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;

    public UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator(
      ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(instance1, null), 
        "a != null should return true", violations);
      RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(null, instance1), 
        "null != a should return true", violations);
      
      T null1 = null;
      T null2 = null;
      RecordedAssertions.False(null1 != null2, "null != null should be false", violations);
    }
  }
}