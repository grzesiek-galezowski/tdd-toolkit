using System.Collections;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;

namespace TddEbook.TddToolkitSpecification
{
  public class ReceivedNothingSpecification
  {
    [Test]
    public void ShouldPassWhenNoCallsWereMade()
    {
      var sub = Substitute.For<IEnumerable>();
      Assert.DoesNotThrow(() => sub.ReceivedNothing());
    }

    [Test]
    public void ShouldThrowWhenAnyCallsWereMade()
    {
      var sub = Substitute.For<IList>();
      sub.GetEnumerator();

      Assert.Throws<CallSequenceNotFoundException>(() => sub.ReceivedNothing());
    }
  }
}
