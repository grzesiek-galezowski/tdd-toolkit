using System;
using System.Linq;
using ConventionsFixture;
using NUnit.Framework;
using TddEbook.TddToolkit.Conventions;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AllExceptionsNamesMustEndWithExceptionSpecification
  {
    [Test]
    public void ShouldFailWhenPassedAnAssemlyToWhichSourceHasReference()
    {
      var types = Types.InAssemblyOf(typeof(AssemblyIdType));
      var convention = new AllExceptionsNamesMustEndWithException();

      //WHEN-THEN
      var exception = Assert.Throws<ConventionFailedException>(() =>
      {
        Convention.Is(convention, types);
      });
      StringAssert.StartsWith(
        "'All types inheriting from exception must end with 'Exception'' for 'Types in ConventionsFixture'",
        exception.Message);
      StringAssert.EndsWith("ConventionsFixture.ExceptionWithoutExceptionSuffix\r\n",
        exception.Message);
      Assert.AreEqual(1, OccurencesOfString("ConventionsFixture", exception.Message));
    }

    [Test]
    public void ShouldPassWhenPassedAnAssemlyToWhichSourceHasNoReference()
    {
      var types = Types.InAssemblyOf(typeof(AssemblyIdType));
      var enumerable = types.Where(t => t != typeof(ExceptionWithoutExceptionSuffix)).ToArray();
      types = Types.InCollection(
        enumerable, types.Description);
      var convention = new AllExceptionsNamesMustEndWithException();

      //WHEN-THEN
      Convention.Is(convention, types);
    }

    private static int OccurencesOfString(string searchedForString, string containingString)
    {
      return containingString.Split(new[] { searchedForString }, StringSplitOptions.RemoveEmptyEntries).Length - 2;
    }

  }
}
