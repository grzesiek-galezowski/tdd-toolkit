using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static int Integer()
    {
      return Generator.Create<int>();
    }

    public static double Double()
    {
      return Generator.Create<double>();
    }

    public static long LongInteger()
    {
      return Generator.Create<long>();
    }

    public static short ShortInteger()
    {
      return Generator.Create<short>();
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return ValueOtherThan(others);
    }

    public static byte Byte()
    {
      return ValueOf<byte>();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return ValueOtherThan(others);
    }

    public static decimal Decimal()
    {
      return ValueOf<decimal>();
    }

    public static decimal DecimalOtherThan(decimal other)
    {
      return ValueOtherThan(other);
    }
  }
}
