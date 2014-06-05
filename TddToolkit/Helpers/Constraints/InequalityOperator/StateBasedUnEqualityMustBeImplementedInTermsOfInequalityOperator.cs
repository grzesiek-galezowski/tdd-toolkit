using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkit.Helpers.Constraints.InequalityOperator
{
  public class StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator
    : IConstraint
  {
    private readonly ValueObjectActivator _activator;
    private readonly int[] _indexesOfConstructorArgumentsToSkip;

    public StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator(
      ValueObjectActivator activator, int[] indexesOfConstructorArgumentsToSkip)
    {
      _activator = activator;
      _indexesOfConstructorArgumentsToSkip = indexesOfConstructorArgumentsToSkip;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        if (ArgumentIsPartOfValueIdentity(i))
        {
          var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);

          RecordedAssertions.DoesNotThrow(() =>
            RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, instance2), 
              "a != b should return true if both are created with different argument" + i, violations),
              "a != b should return true if both are created with different argument" + i, violations);
          RecordedAssertions.DoesNotThrow(() =>
            RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(_activator.TargetType, instance1, instance2), 
              "b != a should return true if both are created with different argument" + i, violations),
              "b != a should return true if both are created with different argument" + i, violations);
        }
      }
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !_indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}