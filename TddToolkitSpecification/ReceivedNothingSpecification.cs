using System.Collections;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;
using TddEbook.TddToolkit.Nunit.NUnitExtensions;

namespace TddEbook.TddToolkitSpecification
{
  public class ReceivedNothingSpecification
  {
    [Test]
    public void ShouldPassWhenNoCallsWereMade(
      [Substitute] IEnumerable sub)
    {
      Assert.DoesNotThrow(() => sub.ReceivedNothing());
    }

    [Test]
    public void ShouldThrowWhenAnyCallsWereMade(
      [Substitute] IList sub)
    {
      sub.GetEnumerator();

      Assert.Throws<CallSequenceNotFoundException>(() => sub.ReceivedNothing());
    }
  }
}
