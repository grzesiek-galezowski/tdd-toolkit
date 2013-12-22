using NSubstitute;

namespace TddEbook.TddToolkit.NSubstitute
{
  public class XArg
  {
    public static T IsLike<T>(T expected)
    {
      return Arg.Is<T>(arg => Are.Alike(expected, arg));
    }
  }
}
