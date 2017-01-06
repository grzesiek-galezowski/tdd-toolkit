using System;
using System.Numerics;
using System.Text;
using MiscUtil;

namespace TddEbook.TddToolkit
{
  public static class NumericTraits
  {
    public static NumericTraits<int> Integer()
    {
      return new NumericTraits<int>(int.MaxValue);
    }

    public static NumericTraits<long> Long()
    {
      return new NumericTraits<long>(long.MaxValue);
    }

    public static NumericTraits<uint> UnsignedInteger()
    {
      return new NumericTraits<uint>(uint.MaxValue);
    }

    public static NumericTraits<ulong> UnsignedLong()
    {
      return new NumericTraits<ulong>(ulong.MaxValue);
    }
  }
  public class NumericTraits<T>
  {
    public NumericTraits(BigInteger maxValue)
    {
      Max = maxValue;
      MaxValueString = Max.ToString();
      MaxPossibleDigitsCount = MaxValueString.Length;
    }


    private BigInteger Max { get; }
    private int MaxPossibleDigitsCount { get; }
    private string MaxValueString { get; }

    public T GenerateWithExactNumberOfDigits(int digitsCount, Random randomGenerator)
    {
      if (digitsCount > MaxPossibleDigitsCount)
      {
        throw new ArgumentOutOfRangeException(nameof(digitsCount), digitsCount,
          $"expected at most {MaxPossibleDigitsCount}");
      }
      var bytes = GetRandomDigits(digitsCount, randomGenerator);
      var bigInteger = NarrowDownToSpecificNumericTypeRange(bytes);
      var convertedNumber = Operator.Convert<BigInteger, T>(bigInteger);
      return convertedNumber;
    }

    private static string GetRandomDigits(int digitsCount, Random randomGenerator)
    {
      var str = "";
      str += randomGenerator.Next(1, 9);
      var builder = new StringBuilder();
      builder.Append(str);
      for (int i = 1; i < digitsCount; i++)
      {
        builder.Append(randomGenerator.Next(0, 9));
      }
      str = builder.ToString();
      return str;
    }

    private static BigInteger MinimumValueWithSpecifiedDigits(int length)
    {
      var result = "1";
      var builder = new StringBuilder();
      builder.Append(result);
      for (int i = 1; i < length; ++i)
      {
        builder.Append(@"0");
      }
      result = builder.ToString();
      return BigInteger.Parse(result);
    }

    private BigInteger NarrowDownToSpecificNumericTypeRange(string number)
    {
      var bigInteger = BigInteger.Parse(number);
      var min = MinimumValueWithSpecifiedDigits(number.Length);
      var narrowDownToSpecificNumericTypeRange = bigInteger % (Max - min) + min;
      return narrowDownToSpecificNumericTypeRange;
    }
  }
}