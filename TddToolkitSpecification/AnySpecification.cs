using NUnit.Framework;
using TddEbook.TddToolkit;

namespace TddToolkitSpecification
{
  public class AnySpecification
  {
    [Test]
    public void ShouldGenerateDifferentIntegerEachTime()
    {
      //GIVEN
      var int1 = Any.Integer();
      var int2 = Any.Integer();

      //THEN
      Assert.AreNotEqual(int1, int2);
    }

    //TODO add more
  }
}

