using System;
using System.Linq;
using ConventionsFixture;
using NUnit.Framework;
using TddEbook.TddToolkit.Conventions;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AllClassesHaveAtMostOneConstructorSpecification
  {
    [Test]
    public void ShouldFailWhenPassedAnAssemlyToWhichSourceHasReference()
    {
      var types = Types.InAssemblyOf(typeof(AssemblyIdType));
      var forbiddenAssemblyReference = new AllClassesHaveAtMostOneConstructor();

      //WHEN-THEN
      var exception = Assert.Throws<ConventionFailedException>(() =>
      {
        Convention.Is(forbiddenAssemblyReference, types);
      });
      StringAssert.StartsWith(
        "'Each type must have at most one constructor' for 'Types in ConventionsFixture'", 
        exception.Message);
      StringAssert.EndsWith("ConventionsFixture.FixtureObjectWithTwoConstructors\r\n", 
        exception.Message);
      Assert.AreEqual(1, OccurencesOfString("ConventionsFixture", exception.Message));
    }

    [Test]
    public void ShouldPassWhenPassedAnAssemlyToWhichSourceHasNoReference()
    {
      var types = Types.InAssemblyOf(typeof(AssemblyIdType));
      var enumerable = types.Where(t => t != typeof(FixtureObjectWithTwoConstructors)).ToArray();
      types = Types.InCollection(
        enumerable, types.Description);
      var forbiddenAssemblyReference = new AllClassesHaveAtMostOneConstructor();

      //WHEN-THEN
      Convention.Is(forbiddenAssemblyReference, types);
    }


    private static int OccurencesOfString(string searchedForString, string containingString)
    {
      return containingString.Split(new[] { searchedForString }, StringSplitOptions.RemoveEmptyEntries).Length - 2;
    }

  }
}
