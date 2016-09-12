using System;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class ProperValueType : IEquatable<ProperValueType>
  {
    public bool Equals(ProperValueType other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _a == other._a && Equals(_anArray, other._anArray);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ProperValueType) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_a*397) ^ (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    public static bool operator ==(ProperValueType left, ProperValueType right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(ProperValueType left, ProperValueType right)
    {
      return !Equals(left, right);
    }

    private readonly int _a;
    private readonly int[] _anArray;

    public ProperValueType(int a, int[] anArray)
    {
      _a = a;
      _anArray = anArray;
    }
  }
}