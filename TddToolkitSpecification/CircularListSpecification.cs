using System.Collections.Generic;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddToolkitSpecification
{
  public class CircularListSpecification
  {
    [Test]
    public void ShouldReturnAllElementsInOrderTheyWereAdded()
    {
      //GIVEN
      var element1 = Any.Integer();
      var element2 = Any.Integer();
      var element3 = Any.Integer();
      var list = new CircularList<int>(element1, element2, element3);
      //WHEN

      var returnedElement1 = list.Next();
      var returnedElement2 = list.Next();
      var returnedElement3 = list.Next();

      //THEN
      XAssert.Equal(returnedElement1, element1);
      XAssert.Equal(returnedElement2, element2);
      XAssert.Equal(returnedElement3, element3);
    }

    [Test]
    public void ShouldStartOverReturningElementsWhenItRunsOutOfElements()
    {
      //GIVEN
      var element1 = Any.Integer();
      var element2 = Any.Integer();
      var list = new CircularList<int>(element1, element2);
      //WHEN

      var returnedElement1 = list.Next();
      var returnedElement2 = list.Next();
      var returnedElement3 = list.Next();
      var returnedElement4 = list.Next();

      //THEN
      XAssert.Equal(returnedElement1, element1);
      XAssert.Equal(returnedElement2, element2);
      XAssert.Equal(returnedElement3, element1);
      XAssert.Equal(returnedElement4, element2);
    }
  }
}
