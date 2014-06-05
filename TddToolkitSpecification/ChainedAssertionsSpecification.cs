using NUnit.Framework;
using TddEbook.TddToolkit.ChainedAssertions;

namespace TddEbook.TddToolkitSpecification
{
  public class ChainedAssertionsSpecification
  {
    [Test]
    public void ShouldAllowTestingForNotNullWithMessage()
    {
      //GIVEN
      var levels = new Level1();

      //THEN
      var e = Assert.Throws<AssertionException>(() =>
        levels.AssertNotNull("l1")
          ._l2.AssertNotNull("l2")
          ._l3.AssertNotNull("l3")
          ._l4.AssertNotNull("l4")
          ._l5.AssertNotNull("l5")
        );
      StringAssert.Contains("l5", e.Message);
    }

    [Test]
    public void ShouldAllowTestingForEqualityWithMessage()
    {
      Level4.Str.AssertEqualTo("aaa", "l4str");

      var e = Assert.Throws<AssertionException>(() =>
        Level4.Str.AssertNotNull("l4NotNull")
          .AssertEqualTo("abc", "l4str")

        );
      StringAssert.Contains("l4str", e.Message);

    }

    [Test]
    public void ShouldAllowTestingForGreaterLower()
    {
      5.AssertEqualTo(5, "e")
        .AssertGreaterOrEqualTo(5, "ge")
        .AssertGreaterOrEqualTo(4, "ge2")
        .AssertGreaterThan(4, "g")
        .AssertLessOrEqualTo(5, "le")
        .AssertLessOrEqualTo(6, "le2")
        .AssertLessThan(6, "l")
        .AssertPositive("positive");
    }

    [Test]
    public void ShouldAllowAssertingOnCollections()
    {
      //GIVEN
      var collection = new[] {"Amber", "Annie", "Eve", "Julie"};

      //THEN
      var e = Assert.Throws<AssertionException>(() => collection
                                                        .AssertContains("Annie", "Annie")
                                                        .AssertDoesNotContain("Jodie", "should not contain Jodie")
                                                        .AssertContains<string[], string>(s => s.StartsWith("Ju"), "predicate1")
                                                        .AssertIsNotEmpty("not empty")
                                                        .AssertIsInAcendingOrder("ascending order")
                                                        [0]
                                                        .AssertEqualTo("Amber", "Amber")
                                                        .AssertEqualTo("Lol", "This should fail"));
      StringAssert.Contains("This should fail", e.Message);
    }

    public class Level1
    {
      public readonly Level2 _l2 = new Level2();
    }

    public class Level2
    {
      public readonly Level3 _l3 = new Level3();
    }

    public class Level3
    {
      public readonly Level4 _l4 = new Level4();
    }

    public class Level4
    {
      public readonly Level5 _l5 = null;
      public const string Str = "aaa";
    }

    public class Level5
    {

    }
  }

}
