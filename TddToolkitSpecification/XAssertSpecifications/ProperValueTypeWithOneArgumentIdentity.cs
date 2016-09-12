using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class ProperValueTypeWithOneArgumentIdentity : IEquatable<ProperValueTypeWithOneArgumentIdentity>
  {
    public bool Equals(ProperValueTypeWithOneArgumentIdentity other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(_anArray, other._anArray);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ProperValueTypeWithOneArgumentIdentity) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    public static bool operator ==(
      ProperValueTypeWithOneArgumentIdentity left, ProperValueTypeWithOneArgumentIdentity right)
    {
      if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
      {
        return true;
      }
      else if (ReferenceEquals(left, null))
      {
        return false;
      }
      else
      {
        return left.Equals(right);
      }
    }

    public static bool operator !=(
      ProperValueTypeWithOneArgumentIdentity left, ProperValueTypeWithOneArgumentIdentity right)
    {
      return !Equals(left, right);
    }

    private readonly int _a;
    private readonly IEnumerable<int> _anArray;

    public ProperValueTypeWithOneArgumentIdentity(int a, IEnumerable<int> anArray)
    {
      _a = a;
      _anArray = anArray;
    }
  }
}