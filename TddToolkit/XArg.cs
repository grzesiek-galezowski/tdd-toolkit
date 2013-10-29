using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;

namespace TddEbook.TddToolkit
{
  public class XArg
  {
    public static T IsLike<T>(T expected)
    {
      return Arg.Is<T>(arg => Are.Alike(expected, arg));
    }
  }
}
