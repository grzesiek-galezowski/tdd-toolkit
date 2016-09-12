using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class ProperValueTypeWithoutEqualityOperator : IEquatable<ProperValueTypeWithoutEqualityOperator>
  {
    public bool Equals(ProperValueTypeWithoutEqualityOperator other)
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
      return Equals((ProperValueTypeWithoutEqualityOperator) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_a*397) ^ (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    private readonly int _a;
    private readonly IEnumerable<int> _anArray;

    public ProperValueTypeWithoutEqualityOperator(int a, IEnumerable<int> anArray)
    {
      _a = a;
      _anArray = anArray;
    }
  }
}