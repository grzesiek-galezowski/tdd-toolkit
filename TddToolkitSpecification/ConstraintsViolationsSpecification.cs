using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkitSpecification
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

    [Test]
    public void ShouldBeAbleToGenerateSeededStrings()
    {
      //WHEN
      const string seed = "xyz";
      var violation1 = Any.String(seed);
      var violation2 = Any.String(seed);

      //THEN
      StringAssert.StartsWith(seed, violation1);
      StringAssert.StartsWith(seed, violation2);
      Assert.AreNotEqual(violation1, violation2);
    }
  }
}
