using System;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkitSpecification.Fixtures;

namespace TddEbook.TddToolkitSpecification
{
  public class CloneSpecification
  {
    [Test]
    public void ShouldCloneCircularLists()
    {
      var circularList = CircularList.CreateStartingFrom0("a", "b", "c");

      var clone = Clone.Of(circularList);

      XAssert.Alike(circularList, clone);
      XAssert.NotSame(circularList, clone);
    }

    [Test]
    public void ShouldHaveNotPropagateChangesFromClonedInstancesToOriginals()
    {
      var data = Any.Instance<ConcreteDataStructure>();

      var prevData = Clone.Of(data);

      Do(data);

      Assert.Throws<AssertionException>(() =>
      {
        XAssert.Alike(prevData, data);
      });
    }

    private void Do(ConcreteDataStructure data)
    {
      data.Span = TimeSpan.FromDays(12334);
    }
  }
}
