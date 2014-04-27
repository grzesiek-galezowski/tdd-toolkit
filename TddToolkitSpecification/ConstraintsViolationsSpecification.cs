using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddToolkitSpecification
{
  public class ConstraintsViolationsSpecification
  {
    [Test]
    public void ShouldNotThrowExceptionWhenNoViolationsHaveBeenAdded()
    {
      //GIVEN
      var violations = new ConstraintsViolations();

      //WHEN - THEN
      Assert.DoesNotThrow(violations.AssertNone);
    }

    [Test]
    public void ShouldThrowExceptionWhenAtLeastOneViolationWasAdded()
    {
      //GIVEN
      var violations = new ConstraintsViolations();
      violations.Add(Any.String());

      //WHEN - THEN
      Assert.Throws<AssertionException>(violations.AssertNone);
    }

    [Test]
    public void ShouldThrowExceptionContainingAllViolationMessagesWhenMoreThanOneViolationWasAdded()
    {
      //GIVEN
      var violations = new ConstraintsViolations();
      var violation1 = Any.String();
      var violation2 = Any.String();
      var violation3 = Any.String();
      violations.Add(violation1);
      violations.Add(violation2);
      violations.Add(violation3);

      //WHEN - THEN
      var exception = Assert.Throws<AssertionException>(violations.AssertNone);
      StringAssert.Contains(violation1, exception.Message);
      StringAssert.Contains(violation2, exception.Message);
      StringAssert.Contains(violation3, exception.Message);
    }
  }
}
