using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ChainedAssertions;

namespace TddToolkitSpecification
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
      //GIVEN
      var levels = new Level1();

      //THEN
      levels.AssertNotNull("l1")
        ._l2.AssertNotNull("l2")
        ._l3.AssertNotNull("l3")
        ._l4.AssertNotNull("l4")
        ._str.AssertEqualTo("aaa", "l4str");

      var e = Assert.Throws<AssertionException>(() =>
        levels.AssertNotNull("l1")
          ._l2.AssertNotNull("l2")
          ._l3.AssertNotNull("l3")
          ._l4.AssertNotNull("l4")
          ._str.AssertNotNull("l4NotNull")
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
        .AssertLessThan(6, "l");
    }

    public class Level1
    {
      public Level2 _l2 = new Level2();
    }

    public class Level2
    {
      public Level3 _l3 = new Level3();
    }

    public class Level3
    {
      public Level4 _l4 = new Level4();
    }

    public class Level4
    {
      public Level5 _l5 = null;
      public string _str = "aaa";
    }

    public class Level5
    {

    }
  }

}
