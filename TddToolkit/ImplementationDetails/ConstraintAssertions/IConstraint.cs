using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;
namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public interface IConstraint
  {
    void CheckAndRecord(ConstraintsViolations violations);
  }
}