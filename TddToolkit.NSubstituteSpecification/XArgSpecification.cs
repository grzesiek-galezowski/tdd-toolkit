using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.NSubstitute;

namespace TddToolkitSpecification
{
  public class XArgSpecification
  {
    [Test]
    public void ShouldWorkCorrectlyWithNSubstitute()
    {
      var s = Substitute.For<IXYZ>();
      s.Do(new List<int>());

      s.Received(1).Do(XArg.IsLike(new List<int>()));
      s.DidNotReceive().Do(XArg.IsLike(new List<int>() { 1 }));
    }

  }


  public interface IXYZ
  {
    void Do(IEnumerable<int> ints);
  }

}
