using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityShouldBeAvailableInTermsOfEqualityOperator<T> 
    : IConstraint where T : class
  {
    public void CheckAndRecord(ConstraintsViolations violations)
    {
 	    XAssert.IsEqualityOperatorDefinedFor<T>();
    }
}
}
