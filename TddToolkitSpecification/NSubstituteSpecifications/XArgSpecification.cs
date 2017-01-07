using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;
using TddEbook.TddToolkit.NSubstitute.ImplementationDetails;

namespace TddEbook.TddToolkitSpecification.NSubstituteSpecifications
{
  public class XArgSpecification
  {
    [Test]
    public void ShouldCorrectlyReportLikenessWithNSubstitute()
    {
      var s = Substitute.For<IXyz>();
      s.Do(new List<int>());

      s.Received(1).Do(XArg.IsLike(new List<int>()));
      s.DidNotReceive().Do(XArg.IsLike(new List<int> { 1 }));
    }

    [Test]
    public void ShouldCorrectlyReportUnlikenessWithNSubstitute()
    {
      var s = Substitute.For<IXyz>();
      s.Do(new List<int>());

      var e = Assert.Throws<ReceivedCallsException>(() =>
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
      var s = Substitute.For<IXyz>();
      s.Do(new List<int>());
      s.Received(1).Do(XArg.IsEquivalentTo(new List<int>()));
    }

    [Test]
    public void ShouldCorrectlyReportCollectionEquivalencyError()
    {
      var s = Substitute.For<IXyz>();
      s.Do(new List<int>());
      Assert.Throws<ReceivedCallsException>(() =>
      {
        s.Received(1).Do(XArg.IsEquivalentTo(new List<int>() {1, 2, 3}));
      });
    }

    [Test]
    public void ShouldCorrectlyReportCompoundCollectionEquivalencyErrors()
    {
      var s = Substitute.For<IXyz>();
      s.Do(new List<int>() {1});
      var exception = Assert.Throws<ReceivedCallsException>(() =>
      {
        s.Received(1).Do(XArg.IsEquivalentTo(new List<int>() {8},
          new ObjectComparerEquivalencyAssertion(),
          FluentAssertionsEquivalencyAssertion<List<int>>.Default()));
      });
      exception.Message.Should().Contain("arg[0]: 2 assertion(s) failed");
      exception.Message.Should().Contain("=== FAILED ASSERTION 1 DETAILS ===");
      exception.Message.Should().Contain("Expected[0] != Actual[0], Values (8,1)");
      exception.Message.Should().Contain("=== FAILED ASSERTION 2 DETAILS ===");
      exception.Message.Should().Contain("Expected item[0] to be 8, but found 1");
    }

    [Test]
    public void ShouldLetArgumentPassWhenItPassesSpecifiedAssertions()
    {
      //GIVEN
      var xyz = Substitute.For<IXyz>();

      //WHEN
      xyz.Do(new List<int>() { 1,2,3 });
      xyz.Do(new List<int>() { 6,5,4 });

      //THEN
      xyz.Received(1).Do(XArg.Passing<List<int>>(
        l => l.Should().BeInAscendingOrder(),
        l => l.Should().Contain(1),
        l => l.Should().Contain(2),
        l => l.Should().Contain(3)));

      xyz.Received(1).Do(XArg.Passing<List<int>>(
        l => l.Should().BeInDescendingOrder(),
        l => l.Should().Contain(6),
        l => l.Should().Contain(5),
        l => l.Should().Contain(4)));
    }

    [Test]
    public void ShouldNotLetArgumentPassWhenItDoesNotPassSpecifiedAssertions()
    {
      //GIVEN
      var xyz = Substitute.For<IXyz>();

      //WHEN
      xyz.Do(new List<int>() { 1,2,3 });

      //THEN
      var exception = Assert.Throws<ReceivedCallsException>(() =>
      {
        xyz.Received(1).Do(XArg.Passing<List<int>>(
          l => l.Should().BeInDescendingOrder(),
          l => l.Should().Contain(4),
          l => l.Should().Contain(5),
          l => l.Should().Contain(1),
          l => l.Should().Contain(6)));
      });

      exception.Message.Should().Contain("4 assertion(s) failed");
      exception.Message.Should().Contain("=== FAILED ASSERTION 1 DETAILS ===");
      exception.Message.Should().Contain("=== FAILED ASSERTION 2 DETAILS ===");
      exception.Message.Should().Contain("=== FAILED ASSERTION 3 DETAILS ===");
      exception.Message.Should().Contain("=== FAILED ASSERTION 5 DETAILS ===");
      exception.Message.Should().Contain("Expected collection to contain items in descending order, but found {1, 2, 3} where item at index 0 is in wrong order");
      exception.Message.Should().Contain("Expected collection {1, 2, 3} to contain 4");
      exception.Message.Should().Contain("Expected collection {1, 2, 3} to contain 5");
      exception.Message.Should().Contain("Expected collection {1, 2, 3} to contain 6");
      exception.Message.Should().NotContain("=== FAILED CONDITION 4 ===");
    }
  }

  public interface IXyz
  {
    void Do(IEnumerable<int> ints); 
    void Do2(int x, int y); 
  }

}
