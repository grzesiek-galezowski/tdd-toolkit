using System;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator : IConstraint
  {
    private Type _type;

    public StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator(Type type)
    {
      _type = type;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
 	    XAssert.IsInequalityOperatorDefinedFor(_type);
    }
}
}
