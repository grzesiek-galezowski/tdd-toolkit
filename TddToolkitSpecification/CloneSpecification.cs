using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkitSpecification
{
  public class CloneSpecification
  {


    [Test]
    public void ShouldCloneCircularLists()
    {
      var circularList = new CircularList<string>("a", "b", "c");

      var clone = Clone.Of(circularList);

      XAssert.Alike(circularList, clone);
      XAssert.NotSame(circularList, clone);
    }
  }
}
