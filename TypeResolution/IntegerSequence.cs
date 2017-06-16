using System;

namespace TddEbook.TddToolkit.TypeResolution
{
  public class IntegerSequence : IEquatable<IntegerSequence>
  {
    public bool Equals(IntegerSequence other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _startingValue == other._startingValue && _step == other._step;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((IntegerSequence) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_startingValue*397) ^ _step;
      }
    }

    public static bool operator ==(IntegerSequence left, IntegerSequence right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(IntegerSequence left, IntegerSequence right)
    {
      return !Equals(left, right);
    }

    private readonly int _startingValue;
    private readonly int _step;
    private int _currentValue;

    public IntegerSequence(int startingValue, int step, int initialStepsCount)
    {
      _startingValue = startingValue;
      _step = step;

      try
      {
        _currentValue = _startingValue + initialStepsCount * _step;
      }
      catch (OverflowException)
      {
        _currentValue = _startingValue;
      }
      
    }

    public int Next()
    {
      var result = _currentValue;
      try
      {
        _currentValue += _step;
        if (OverflowHappened())
        {
          Reset();
        }
      }
      catch (Exception)
      {
        Reset();
      }

      return result;
    }

    private void Reset()
    {
      _currentValue = _startingValue;
    }

    private bool OverflowHappened()
    {
      return _currentValue < _startingValue;
    }
  }
}