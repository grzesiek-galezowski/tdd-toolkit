namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static int Integer()
    {
      return Generate.Integer();
    }

    public static double Double()
    {
      return Generate.Double();
    }

    public static double DoubleOtherThan(params double[] others)
    {
      return Generate.DoubleOtherThan(others);
    }

    public static long LongInteger()
    {
      return Generate.LongInteger();
    }

    public static long LongIntegerOtherThan(params long[] others)
    {
      return Generate.LongIntegerOtherThan(others);
    }

    public static ulong UnsignedLongInteger()
    {
      return Generate.UnsignedLongInteger();
    }

    public static ulong UnsignedLongIntegerOtherThan(params ulong[] others)
    {
      return Generate.UnsignedLongIntegerOtherThan(others);
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return Generate.IntegerOtherThan(others);
    }

    public static byte Byte()
    {
      return Generate.Byte();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return Generate.ByteOtherThan(others);
    }

    public static decimal Decimal()
    {
      return Generate.Decimal();
    }

    public static decimal DecimalOtherThan(params decimal[] others)
    {
      return Generate.DecimalOtherThan(others);
    }

    public static uint UnsignedInt()
    {
      return Generate.UnsignedInt();
    }

    public static uint UnsignedIntOtherThan(params uint[] others)
    {
      return Generate.UnsignedIntOtherThan(others);
    }

    public static ushort UnsignedShort()
    {
      return Generate.UnsignedShort();
    }

    public static ushort UnsignedShortOtherThan(params ushort[] others)
    {
      return Generate.UnsignedShortOtherThan(others);
    }

    public static short ShortInteger()
    {
      return Generate.ShortInteger();
    }

    public static short ShortIntegerOtherThan(params short[] others)
    {
      return Generate.ShortIntegerOtherThan(others);
    }

    public static byte Digit()
    {
      return Generate.Digit();
    }

    public static int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return Generate.IntegerFromSequence(startingValue, step);
    }

    public static byte Octet()
    {
      return Generate.Octet();
    }

    public static int IntegerDivisibleBy(int quotient)
    {
      return Generate.IntegerDivisibleBy(quotient);
    }

    public static int IntegerNotDivisibleBy(int quotient)
    {
      return Generate.IntegerNotDivisibleBy(quotient);
    }

    public static int IntegerWithExactDigitsCount(int digitsCount)
    {
      return Generate.IntegerWithExactDigitsCount(digitsCount);
    }

    public static long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return Generate.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public static uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return Generate.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return Generate.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public static byte PositiveDigit()
    {
      return Generate.PositiveDigit();
    }

  }
}
