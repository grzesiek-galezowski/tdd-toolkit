using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects<T> : IConstraint<T>
  {
    private readonly ValueObjectActivator<T> _activator;

    public HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }
    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      var instance3 = _activator.CreateInstanceAsValueObjectWithPreviousParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);
        RecordedAssertions.NotEqual(instance1.GetHashCode(), instance2.GetHashCode(), 
          "b.GetHashCode() and b.GetHashCode() should return different values when both are created with different argument" + i, violations);
      }

      RecordedAssertions.Equal(instance1.GetHashCode(), instance3.GetHashCode(), "a.GetHashCode() and b.GetHashCode() should return same values when both are created with same arguments", violations);
    }
  }
}