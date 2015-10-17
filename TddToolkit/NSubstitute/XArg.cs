using System.Collections.Generic;
using System.Linq;
using NSubstitute;

namespace TddEbook.TddToolkit.NSubstitute
{
  public class XArg
  {
    public static T IsLike<T>(T expected)
    {
      return Arg.Is<T>(arg => Are.Alike(expected, arg));
    }

    public static T IsNotLike<T>(T expected)
    {
      return Arg.Is<T>(arg => !Are.Alike(expected, arg));
    }

    public static T[] IsNot<T>(IEnumerable<T> unexpected)
    {
      return Arg.Is<T[]>(arg => !arg.SequenceEqual(unexpected));
    }
  }
}
