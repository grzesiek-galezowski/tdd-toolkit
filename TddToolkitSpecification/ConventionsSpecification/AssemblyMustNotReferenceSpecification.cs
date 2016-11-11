using ConventionsFixture;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TestStack.ConventionTests;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AssemblyMustNotReferenceSpecification
  {
    [Test]
    public void ShouldFailWhenPassedAnAssemlyToWhichSourceHasReference()
    {
      var sourceAssembly = typeof(AssemblyIdType).Assembly;
      var forbiddenReference = typeof(string).Assembly;
      var reason = Any.String();
      var expectedMessageStart = ExpectedAssemblyRefConventionMessage.Start(forbiddenReference, sourceAssembly, reason);
      var expectedMessageEnd = ExpectedAssemblyRefConventionMessage.End(sourceAssembly);
      var forbiddenAssemblyReference = new AssemblyDoesNotContainForbiddenReferences(
        forbiddenReference, reason);

      //WHEN-THEN
      var exception = Assert.Throws<ConventionFailedException>(() =>
      {
        Convention.Is(forbiddenAssemblyReference,
          new Assemblies(sourceAssembly));
      });
      StringAssert.StartsWith(expectedMessageStart, exception.Message);
      StringAssert.EndsWith(expectedMessageEnd, exception.Message);
    }

    [Test]
    public void ShouldPassWhenPassedAnAssemlyToWhichSourceHasNoReference()
    {
      var forbiddenAssemblyReference = new AssemblyDoesNotContainForbiddenReferences(
        typeof(TestAttribute).Assembly, "--REASON--");
      Convention.Is(forbiddenAssemblyReference,
        new Assemblies(typeof(AssemblyIdType).Assembly));
    }

  }
}
