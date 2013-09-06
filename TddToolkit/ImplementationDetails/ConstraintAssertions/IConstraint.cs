namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public interface IConstraint<T>
  {
    void CheckAndRecord(ConstraintsViolations violations);
  }
}