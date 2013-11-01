using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using System.Linq;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects<T> : IConstraint
  {
    private readonly ValueObjectActivator<T> _activator;
    private readonly int[] _indexesOfConstructorArgumentsToSkip;
    
    public HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects(
      ValueObjectActivator<T> activator,
      int[] indexesOfConstructorArgumentsToSkip)
    {
      _activator = activator;
      _indexesOfConstructorArgumentsToSkip = indexesOfConstructorArgumentsToSkip;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      var instance3 = _activator.CreateInstanceAsValueObjectWithPreviousParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        if (ArgumentIsPartOfValueIdentity(i))
        {
          var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);
          RecordedAssertions.NotEqual(instance1.GetHashCode(), instance2.GetHashCode(),
            "b.GetHashCode() and b.GetHashCode() should return different values when both are created with different argument" + i, violations);
        }
      }

      RecordedAssertions.Equal(instance1.GetHashCode(), instance3.GetHashCode(), "a.GetHashCode() and b.GetHashCode() should return same values when both are created with same arguments", violations);
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !this._indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}