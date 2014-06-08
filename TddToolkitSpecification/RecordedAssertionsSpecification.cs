using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkitSpecification
{
  class RecordedAssertionsSpecification
  {
    [Test]
    public void ShouldAddErrorMessageWhenTruthAssertionFails()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(false, anyMessage, violations);

      //THEN
      violations.Received(1).Add(anyMessage);
    }

    [Test]
    public void ShouldNotAddErrorMessageWhenTruthAssertionPasses()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(true, anyMessage, violations);

      //THEN
      violations.DidNotReceive().Add(anyMessage);
    }


    [Test]
    public void ShouldFailStaticFieldsAssertionIfAssemblyContainsAtLeastOneStaticField()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      
      var e = Assert.Throws<AssertionException>(() => XAssert.NoStaticFieldsIn(assembly));
      StringAssert.Contains("_lolek", e.Message);
      StringAssert.Contains("_gieniek", e.Message);
    }

    [Test]
    public void ShouldFailReferenceAssertionWhenAssemblyReferencesOtherAssembly()
    {
      var assembly1 = typeof(RecordedAssertionsSpecification).Assembly;
      Assert.Throws<AssertionException>(() => XAssert.IsNotReferencedBy(assembly1, typeof(TestAttribute)));
    }

    public class Lol2
    {
      private static int _gieniek = 123;
    }

#pragma warning disable 169
    private static int _lolek = 12;
#pragma warning restore 169

  }


}
