using System;
using System.Collections.Generic;
using System.Linq;
using CommonTypes;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static int Integer()
    {
      return _any.Integer();
    }

    public static double Double()
    {
      return _any.Double();
    }

    public static double DoubleOtherThan(params double[] others)
    {
      return _any.DoubleOtherThan(others);
    }

    public static long LongInteger()
    {
      return _any.LongInteger();
    }

    public static long LongIntegerOtherThan(params long[] others)
    {
      return _any.LongIntegerOtherThan(others);
    }

    public static ulong UnsignedLongInteger()
    {
      return _any.UnsignedLongInteger();
    }

    public static ulong UnsignedLongIntegerOtherThan(params ulong[] others)
    {
      return _any.UnsignedLongIntegerOtherThan(others);
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return _any.IntegerOtherThan(others);
    }

    public static byte Byte()
    {
      return _any.Byte();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return _any.ByteOtherThan(others);
    }

    public static decimal Decimal()
    {
      return _any.Decimal();
    }

    public static decimal DecimalOtherThan(params decimal[] others)
    {
      return _any.DecimalOtherThan(others);
    }

    public static uint UnsignedInt()
    {
      return _any.UnsignedInt();
    }

    public static uint UnsignedIntOtherThan(params uint[] others)
    {
      return _any.UnsignedIntOtherThan(others);
    }

    public static ushort UnsignedShort()
    {
      return _any.UnsignedShort();
    }

    public static ushort UnsignedShortOtherThan(params ushort[] others)
    {
      return _any.UnsignedShortOtherThan(others);
    }

    public static short ShortInteger()
    {
      return _any.ShortInteger();
    }

    public static short ShortIntegerOtherThan(params short[] others)
    {
      return _any.ShortIntegerOtherThan(others);
    }

    public static byte Digit()
    {
      return _any.Digit();
    }

    public static int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return _any.IntegerFromSequence(startingValue, step);
    }

    public static byte Octet()
    {
      return _any.Octet();
    }

    public static int IntegerDivisibleBy(int quotient)
    {
      return _any.IntegerDivisibleBy(quotient);
    }

    public static int IntegerNotDivisibleBy(int quotient)
    {
      return _any.IntegerNotDivisibleBy(quotient);
    }

    private static void AssertQuotientMakesSense(int quotient)
    {
      _any.AssertQuotientMakesSense(quotient);
    }


    public static int IntegerWithExactDigitsCount(int digitsCount)
    {
      return _any.IntegerWithExactDigitsCount(digitsCount);
    }

    public static long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _any.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public static uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return _any.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _any.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }


    public static byte PositiveDigit()
    {
      return _any.PositiveDigit();
    }

  }
}
