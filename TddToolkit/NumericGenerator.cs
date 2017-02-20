using System;
using System.Collections.Generic;
using System.Linq;
using CommonTypes;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit
{
  public class NumericGenerator
  {
    private readonly AllGenerator _allGenerator;

    private readonly CircularList<byte> _digits =
      CircularList.CreateStartingFromRandom(new byte[] {5, 6, 4, 7, 3, 8, 2, 9, 1, 0});

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly HashSet<IntegerSequence> _sequences = new HashSet<IntegerSequence>();

    private readonly CircularList<int> _numbersToMultiply = CircularList.CreateStartingFromRandom(
      System.Linq.Enumerable.Range(1, 100).ToArray());

    private readonly NumericTraits<int> _intTraits = NumericTraits.Integer();
    private readonly NumericTraits<long> _longTraits = NumericTraits.Long();
    private readonly NumericTraits<uint> _uintTraits = NumericTraits.UnsignedInteger();
    private readonly NumericTraits<ulong> _ulongTraits = NumericTraits.UnsignedLong();

    public NumericGenerator(AllGenerator allGenerator)
    {
      _allGenerator = allGenerator;
    }

    public byte Digit() => _digits.Next();

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      var sequence = new IntegerSequence(startingValue, step);
      var finalSequence = Maybe.Wrap(_sequences.FirstOrDefault(s => s.Equals(sequence))).ValueOr(sequence);
      _sequences.Add(finalSequence);
      var integerFromSequence = finalSequence.Next();
      return integerFromSequence;
    }

    public byte Octet()
    {
      return Byte();
    }

    public int IntegerDivisibleBy(int quotient)
    {
      return _numbersToMultiply.Next() * quotient;
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      AssertQuotientMakesSense(quotient);
      return IntegerDivisibleBy(quotient) + 1;
    }

    public int IntegerWithExactDigitsCount(int digitsCount) => 
      _intTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public long LongIntegerWithExactDigitsCount(int digitsCount) => 
      _longTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount) => 
      _uintTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount) => 
      _ulongTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public byte PositiveDigit()
    {
      byte digit = Digit();
      while (digit == 0)
      {
        digit = Digit();
      }
      return digit;
    }

    public int Integer() => _allGenerator.ValueOf<int>();
    public double Double() => _allGenerator.ValueOf<double>();
    public double DoubleOtherThan(params double[] others) => _allGenerator.ValueOtherThan(others);
    public long LongInteger() => _allGenerator.ValueOf<long>();
    public long LongIntegerOtherThan(params long[] others) => _allGenerator.ValueOtherThan(others);
    public ulong UnsignedLongInteger() => _allGenerator.ValueOf<ulong>();
    public ulong UnsignedLongIntegerOtherThan(params ulong[] others) => _allGenerator.ValueOtherThan(others);
    public int IntegerOtherThan(params int[] others) => _allGenerator.ValueOtherThan(others);
    public byte Byte() => _allGenerator.ValueOf<byte>();
    public byte ByteOtherThan(params byte[] others) => _allGenerator.ValueOtherThan(others);
    public decimal Decimal() => _allGenerator.ValueOf<decimal>();
    public decimal DecimalOtherThan(params decimal[] others) => _allGenerator.ValueOtherThan(others);
    public uint UnsignedInt() => _allGenerator.ValueOf<uint>();
    public uint UnsignedIntOtherThan(params uint[] others) => _allGenerator.ValueOtherThan(others);
    public ushort UnsignedShort() => _allGenerator.ValueOf<ushort>();
    public ushort UnsignedShortOtherThan(params ushort[] others) => _allGenerator.ValueOtherThan(others);
    public short ShortInteger() => _allGenerator.ValueOf<short>();
    public short ShortIntegerOtherThan(params short[] others) => _allGenerator.ValueOtherThan(others);

    private void AssertQuotientMakesSense(int quotient)
    {
      if (quotient == 1 || quotient == -1 || quotient == 0)
      {
        throw new ArgumentException($"generating an integer not dividable by {quotient} is not supported");
      }
    }
  }
}