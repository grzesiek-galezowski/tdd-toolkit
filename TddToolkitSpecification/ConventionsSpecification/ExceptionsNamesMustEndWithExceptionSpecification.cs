using System;
using ConventionsFixture;
using NUnit.Framework;
using TddEbook.TddToolkit.Conventions;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class ExceptionsNamesMustEndWithExceptionSpecification
  {
    [Test]
    public void ShouldFailWhenPassedAnAssemlyToWhichSourceHasReference()
    {
      var types = Types.InAssemblyOf<AssemblyIdType>();
      var convention = new ExceptionsNamesMustEndWithException();

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
      var types = Types.InAssemblyOf<AssemblyIdType>();
      types = types.Without(typeof(ExceptionWithoutExceptionSuffix));
      var convention = new ExceptionsNamesMustEndWithException();

      //WHEN-THEN
      Convention.Is(convention, types);
    }

    private static int OccurencesOfString(string searchedForString, string containingString)
    {
      return containingString.Split(new[] { searchedForString }, StringSplitOptions.RemoveEmptyEntries).Length - 2;
    }
  }
}
