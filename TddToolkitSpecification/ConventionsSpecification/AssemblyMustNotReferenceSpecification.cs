using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConventionsFixture;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AssemblyMustNotReferenceSpecification
  {
    [Test]
    public void ShouldFailWhenPassedAnAssemlyToWhichSourceHasReference()
    {
      var sourceAssembly = typeof(AssemblyIdType).Assembly;
      var forbiddenReference = typeof(string).Assembly;
      var reason = Any.StringNotContaining("-");
      var forbiddenAssemblyReference = new AssemblyDoesNotContainForbiddenReferences(
        forbiddenReference, reason);

      var exception = Assert.Throws<ConventionFailedException>(() =>
      {
        Convention.Is(forbiddenAssemblyReference,
          new Assemblies(sourceAssembly));
      });
      var expectedMessageStart = ExpectedMessageStart(forbiddenReference, sourceAssembly, reason);
      var expectedMessageEnd = ExpectedMessageEnd(sourceAssembly);

      StringAssert.StartsWith(expectedMessageStart, exception.Message);
      StringAssert.EndsWith(expectedMessageEnd, exception.Message);
    }

    private static string ExpectedMessageStart(Assembly forbiddenReference, Assembly sourceAssembly, string reason)
    {
      return $@"'Forbidden reference to {forbiddenReference} because {reason}' for 'Assemblies in {sourceAssembly}'".Replace("\t", " ");
    }

    private static string ExpectedMessageEnd(Assembly sourceAssembly)
    {
      return $@"{sourceAssembly}{"\r\n"}";
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
