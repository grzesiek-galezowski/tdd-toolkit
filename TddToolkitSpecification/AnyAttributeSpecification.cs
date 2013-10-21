using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.NUnitExtensions;

namespace TddToolkitSpecification
{
  public class AnyAttributeSpecification
  {
    [Test]
    public void ShouldAllowPassingDifferentObjectsAndPrimitivesThroughParameters
    (
      [Any] int anInt,
      [Any] int anInt2, 
      [Any] string aString, 
      [Any] AnySpecification.ISimple interfaceImplementation,
      [Any] IEnumerable<int> anEnumerable, 
      [Any] List<string> concreteList,
      [Any] IEnumerable<AnySpecification.ISimple> interfaceImplementationList,
      [Any] ProperValueType value,
      [AnyOtherThan(3,4)] int nonThree
    )
    {
      XAssert.NotEqual(default(int), anInt);
      XAssert.NotEqual(anInt2, anInt);
      Assert.False(string.IsNullOrEmpty(aString));
      Assert.NotNull(interfaceImplementation);
      Assert.Greater(anEnumerable.Count(), 0);
      Assert.Greater(concreteList.Count(), 0);
      Assert.Greater(concreteList.Count(), 0);

      Assert.Greater(interfaceImplementationList.Count(), 0);
      Assert.NotNull(interfaceImplementationList.ToArray()[0]);
      Assert.NotNull(interfaceImplementationList.ToArray()[1]);
      Assert.NotNull(interfaceImplementationList.ToArray()[2]);

      Assert.NotNull(value);

      XAssert.NotEqual(3, nonThree);
    }

  }
}
