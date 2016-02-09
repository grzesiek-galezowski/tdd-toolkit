using System.Collections.Generic;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly HashSet<IntegerSequence> _sequences = new HashSet<IntegerSequence>();

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

    public static decimal DecimalOtherThan(params decimal[] others)
    {
      return ValueOtherThan(others);
    }

    public static uint UnsignedInt()
    {
      return ValueOf<uint>();
    }

    public static uint UnsignedIntOtherThan(params uint[] others)
    {
      return ValueOtherThan(others);
    }

    public static ushort UnsignedShort()
    {
      return ValueOf<ushort>();
    }

    public static ushort UnsignedShortOtherThan(params ushort[] others)
    {
      return ValueOtherThan(others);
    }

    public static short ShortInteger()
    {
      return ValueOf<short>();
    }

    public static short ShortIntegerOtherThan(params short[] others)
    {
      return ValueOtherThan(others);
    }

    public static int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      var sequence = new IntegerSequence(startingValue, step);
      var finalSequence = Maybe.Wrap(_sequences.FirstOrDefault(s => s.Equals(sequence))).ValueOr(sequence);
      _sequences.Add(finalSequence);
      return finalSequence.Next();
    }

    public static byte Octet()
    {
      return Any.Byte();
    }
  }
}
