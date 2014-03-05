using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod<T> : IConstraint
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