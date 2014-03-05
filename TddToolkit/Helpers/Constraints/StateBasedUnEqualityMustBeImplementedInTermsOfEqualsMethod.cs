using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class StateBasedUnEqualityMustBeImplementedInTermsOfEqualsMethod<T> : IConstraint
  {
    private readonly ValueObjectActivator<T> _activator;
    private readonly int[] _indexesOfConstructorArgumentsToSkip;

    public StateBasedUnEqualityMustBeImplementedInTermsOfEqualsMethod(
      ValueObjectActivator<T> activator, int[] indexesOfConstructorArgumentsToSkip)
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

          RecordedAssertions.False(instance1.Equals(instance2), "a.Equals(b) should return false if both are created with different argument" + i, violations);
          RecordedAssertions.False(instance2.Equals(instance1), "b.Equals(a) should return false if both are created with different argument" + i, violations);
          RecordedAssertions.False(((object)instance1).Equals((object)instance2), "(object)a.Equals((object)b) should return false if both are created with different argument" + i, violations);
          RecordedAssertions.False(((object)instance2).Equals((object)instance1), "(object)b.Equals((object)a) should return false if both are created with different argument" + i, violations);
        }
      }
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !this._indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}