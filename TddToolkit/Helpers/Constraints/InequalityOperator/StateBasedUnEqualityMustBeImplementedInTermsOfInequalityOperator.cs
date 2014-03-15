using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator
    : IConstraint
  {
    private readonly ValueObjectActivator _activator;
    private int[] _indexesOfConstructorArgumentsToSkip;

    public StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator(
      ValueObjectActivator activator, int[] indexesOfConstructorArgumentsToSkip)
    {
      _activator = activator;
      this._indexesOfConstructorArgumentsToSkip = indexesOfConstructorArgumentsToSkip;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        if (ArgumentIsPartOfValueIdentity(i))
        {
          var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);

          RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, instance2), 
            "a != b should return true if both are created with different argument" + i, violations);
          RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, instance2), 
            "b != a should return true if both are created with different argument" + i, violations);
        }
      }
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !this._indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}