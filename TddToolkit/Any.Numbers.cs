namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static int Integer()
    {
      return ValueOf<int>();
    }

    public static double Double()
    {
      return ValueOf<double>();
    }

    public static double DoubleOtherThan(params double[] others)
    {
      return ValueOtherThan(others);
    }

    public static long LongInteger()
    {
      return ValueOf<long>();
    }

    public static long LongIntegerOtherThan(params long[] others)
    {
      return ValueOtherThan(others);
    }

    public static ulong UnsignedLongInteger()
    {
      return ValueOf<ulong>();
    }

    public static ulong UnsignedLongIntegerOtherThan(params ulong[] others)
    {
      return ValueOtherThan(others);
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

    public static uint UnsignedInt()
    {
      return ValueOf<uint>();
    }

    public static uint UnsignedIntOtherThan(uint other)
    {
      return ValueOtherThan(other);
    }

    public static ushort UnsignedShort()
    {
      return ValueOf<ushort>();
    }

    public static ushort UnsignedShortOtherThan(ushort other)
    {
      return ValueOtherThan(other);
    }

    public static short ShortInteger()
    {
      return ValueOf<short>();
    }

    public static short ShortIntegerOtherThan(short other)
    {
      return ValueOtherThan(other);
    }
  }
}
