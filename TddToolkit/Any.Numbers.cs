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
    }

    public static double Double()
    {
    }

    public static double DoubleOtherThan(params double[] others)
    {
    }

    public static long LongInteger()
    {
    }

    public static long LongIntegerOtherThan(params long[] others)
    {
    }

    public static ulong UnsignedLongInteger()
    {
    }

    public static ulong UnsignedLongIntegerOtherThan(params ulong[] others)
    {
    }

    public static int IntegerOtherThan(params int[] others)
    {
    }

    public static byte Byte()
    {
    }

    public static byte ByteOtherThan(params byte[] others)
    {
    }

    public static decimal Decimal()
    {
    }

    public static decimal DecimalOtherThan(params decimal[] others)
    {
    }

    public static uint UnsignedInt()
    {
    }

    public static uint UnsignedIntOtherThan(params uint[] others)
    {
    }

    public static ushort UnsignedShort()
    {
    }

    public static ushort UnsignedShortOtherThan(params ushort[] others)
    {
    }

    public static short ShortInteger()
    {
    }

    public static short ShortIntegerOtherThan(params short[] others)
    {
    }

    public static byte Digit()
    {
    }

    public static int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
    }

    public static byte Octet()
    {
    }

    public static int IntegerDivisibleBy(int quotient)
    {
    }

    public static int IntegerNotDivisibleBy(int quotient)
    {
    }

    private static void AssertQuotientMakesSense(int quotient)
    {
    }


    public static int IntegerWithExactDigitsCount(int digitsCount)
    {
    }

    public static long LongIntegerWithExactDigitsCount(int digitsCount)
    {
    }

    public static uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
    }


    public static byte PositiveDigit()
    {
    }

  }
}
