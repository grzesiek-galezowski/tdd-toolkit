using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;

namespace TddEbook.TddToolkitSpecification.NSubstituteSpecifications
{
  public class XArgSpecification
  {
    [Test]
    public void ShouldCorrectlyReportLikenessWithNSubstitute()
    {
      var s = Substitute.For<Ixyz>();
      s.Do(new List<int>());

      s.Received(1).Do(XArg.IsLike(new List<int>()));
      s.DidNotReceive().Do(XArg.IsLike(new List<int> { 1 }));
    }

    [Test]
    public void ShouldCorrectlyReportUnlikenessWithNSubstitute()
    {
      var s = Substitute.For<Ixyz>();
      s.Do(new List<int>());

      Assert.Throws<ReceivedCallsException>(() =>
      {
        s.Received(1).Do(XArg.IsNotLike(new List<int>()));
      });

      Assert.Throws<ReceivedCallsException>(() =>
      {
        s.DidNotReceive().Do(XArg.IsNotLike(new List<int> {1}));
      });
    }

    [Test]
    public void ShouldCorrectlyReportCollectionEquivalency()
    {
      var s = Substitute.For<Ixyz>();
      s.Do(new List<int>());
      s.Received(1).Do(XArg.IsEquivalentTo(new List<int>()));
    }

    [Test]
    public void ShouldCorrectlyReportCollectionEquivalencyError()
    {
      var s = Substitute.For<Ixyz>();
      s.Do(new List<int>());
      Assert.Throws<ReceivedCallsException>(() =>
      {
        s.Received(1).Do(XArg.IsEquivalentTo(new List<int>() {1, 2, 3}));
      });
    }

    [Test]
    public void ShouldCorrectlyReportCollectionEquivalency123()
    {
      var s = Substitute.For<Ixyz>();
      s.Do(new List<int>() {1});
      var exception = Assert.Throws<ReceivedCallsException>(() =>
      {
        s.Received(1).Do(XArg.IsEquivalentTo(new List<int>() {8},
          new ObjectComparerEquivalencyAssertion(),
          FluentAssertionsEquivalencyAssertion<List<int>>.Default()));
      });
      exception.Message.Should().Contain("arg[0]: 2 condition(s) failed");
      exception.Message.Should().Contain("=== FAILED CONDITION 1 ===");
      exception.Message.Should().Contain("Expected[0] != Actual[0], Values (8,1)");
      exception.Message.Should().Contain("=== FAILED CONDITION 2 ===");
      exception.Message.Should().Contain("Expected item[0] to be 8, but found 1");
    }
  }

  public interface Ixyz
  {
    void Do(IEnumerable<int> ints); 
  }

}
