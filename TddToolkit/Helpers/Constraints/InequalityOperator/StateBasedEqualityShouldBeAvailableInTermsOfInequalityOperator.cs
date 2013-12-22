using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator<T> 
    : IConstraint where T : class
  {
    public void CheckAndRecord(ConstraintsViolations violations)
    {
 	    XAssert.IsInequalityOperatorDefinedFor<T>();
    }
}
}
