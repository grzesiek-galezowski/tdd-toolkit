using System;
using System.Collections.Generic;
using System.Linq;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.TypeResolution;

namespace TddEbook.TddToolkit.Generators
{
  public class NumericGenerator
  {
    private readonly ValueGenerator _valueGenerator;

    private readonly CircularList<byte> _digits =
      CircularList.CreateStartingFromRandom(new byte[] {5, 6, 4, 7, 3, 8, 2, 9, 1, 0});

    private readonly Random _randomGenerator = new Random(Guid.NewGuid().GetHashCode());
    private readonly HashSet<IntegerSequence> _sequences = new HashSet<IntegerSequence>();

    private readonly CircularList<int> _numbersToMultiply = CircularList.CreateStartingFromRandom(
      Enumerable.Range(1, 100).ToArray());

    private readonly NumericTraits<int> _intTraits = NumericTraits.Integer();
    private readonly NumericTraits<long> _longTraits = NumericTraits.Long();
    private readonly NumericTraits<uint> _uintTraits = NumericTraits.UnsignedInteger();
    private readonly NumericTraits<ulong> _ulongTraits = NumericTraits.UnsignedLong();

    public NumericGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public byte Digit() => _digits.Next();

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      var sequence = new IntegerSequence(startingValue, step, Integer());
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

    public int Integer() => _valueGenerator.ValueOf<int>();
    public double Double() => _valueGenerator.ValueOf<double>();
    public double DoubleOtherThan(params double[] others) => _valueGenerator.ValueOtherThan(others);
    public long LongInteger() => _valueGenerator.ValueOf<long>();
    public long LongIntegerOtherThan(params long[] others) => _valueGenerator.ValueOtherThan(others);
    public ulong UnsignedLongInteger() => _valueGenerator.ValueOf<ulong>();
    public ulong UnsignedLongIntegerOtherThan(params ulong[] others) => _valueGenerator.ValueOtherThan(others);
    public int IntegerOtherThan(params int[] others) => _valueGenerator.ValueOtherThan(others);
    public byte Byte() => _valueGenerator.ValueOf<byte>();
    public byte ByteOtherThan(params byte[] others) => _valueGenerator.ValueOtherThan(others);
    public decimal Decimal() => _valueGenerator.ValueOf<decimal>();
    public decimal DecimalOtherThan(params decimal[] others) => _valueGenerator.ValueOtherThan(others);
    public uint UnsignedInt() => _valueGenerator.ValueOf<uint>();
    public uint UnsignedIntOtherThan(params uint[] others) => _valueGenerator.ValueOtherThan(others);
    public ushort UnsignedShort() => _valueGenerator.ValueOf<ushort>();
    public ushort UnsignedShortOtherThan(params ushort[] others) => _valueGenerator.ValueOtherThan(others);
    public short ShortInteger() => _valueGenerator.ValueOf<short>();
    public short ShortIntegerOtherThan(params short[] others) => _valueGenerator.ValueOtherThan(others);

    private void AssertQuotientMakesSense(int quotient)
    {
      if (quotient == 1 || quotient == -1 || quotient == 0)
      {
        throw new ArgumentException($"generating an integer not dividable by {quotient} is not supported");
      }
    }
  }
}