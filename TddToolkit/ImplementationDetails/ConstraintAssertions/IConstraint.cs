using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public interface IConstraint
  {
    void CheckAndRecord(ConstraintsViolations violations);
  }
}