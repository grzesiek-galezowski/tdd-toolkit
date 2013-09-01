using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit
{
  public static partial class Any
  {
    public static int Integer()
    {
      return _generator.Create<int>();
    }

    public static double Double()
    {
      return _generator.Create<double>();
    }

    public static long LongInteger()
    {
      return _generator.Create<long>();
    }

    public static short ShortInteger()
    {
      return _generator.Create<short>();
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return Any.ValueOtherThan(others);
    }

    public static byte Byte()
    {
      return Any.ValueOf<byte>();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return Any.ValueOtherThan(others);
    }

  }
}
