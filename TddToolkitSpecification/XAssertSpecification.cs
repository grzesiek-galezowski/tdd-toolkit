using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TddEbook.TddToolkit;

namespace TddToolkitSpecification
{
  public class XAssertSpecification
  {
    [Test]
    public void ShouldPassValueTypeAssertionForProperValueType()
    {
      XAssert.IsValue<ProperValueType>();
    }

    [Test]
    public void ShouldAllowSpecifyingConstructorArgumentsNotTakenIntoAccountDuringValueBehaviorCheck()
    {
      XAssert.IsValue<ProperValueTypeWithOneArgumentIdentity>(
        ValueTypeTraits.Custom.SkipConstructorArgument(0));

      Assert.Throws<AssertionException>(() => 
        XAssert.IsValue<ProperValueTypeWithOneArgumentIdentity>()
      );
    }

    [Test]
    public void ShouldAcceptProperFullValueTypesAndRejectBadOnes()
    {
      XAssert.IsValue<ProperValueType>();

      Assert.Throws<AssertionException>(() =>
        XAssert.IsValue<ProperValueTypeWithoutEqualityOperator>()
      );
    }

    [Test]
    public void ShouldAggregateMultipleAssertions()
    {
      var exception = Assert.Throws<AssertionException>(() =>
        XAssert.Multiple(assert =>
        {
          assert.Equal(1, 3);
          assert.Equal(2, 44);
          assert.Equal("aa", "123");
          assert.Contains("bb", "aa");
        })
      );

      StringAssert.Contains("Expected object to be 1, but found 3", 
        exception.ToString());
      StringAssert.Contains("Expected object to be 2, but found 44", 
        exception.ToString());
      StringAssert.Contains("Expected object to be \"aa\", but found \"123\"", 
        exception.ToString());
      StringAssert.Contains("Expected string \"bb\" to contain \"aa\"",
        exception.ToString());

    }
  }

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
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ProperValueTypeWithOneArgumentIdentity)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    public static bool operator ==(ProperValueTypeWithOneArgumentIdentity left, ProperValueTypeWithOneArgumentIdentity right)
    {
      if(object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
      {
        return true;
      }
      else if(object.ReferenceEquals(left, null))
      {
        return false;
      }
      else
      {
        return left.Equals(right);
      }
    }

    public static bool operator !=(ProperValueTypeWithOneArgumentIdentity left, ProperValueTypeWithOneArgumentIdentity right)
    {
      return !Equals(left, right);
    }

    private readonly int _a;
    private readonly IEnumerable<int> _anArray;

    public ProperValueTypeWithOneArgumentIdentity(int a, IEnumerable<int> anArray)
    {
      this._a = a;
      _anArray = anArray;
    }
  }

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
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ProperValueType)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_a * 397) ^ (_anArray != null ? _anArray.GetHashCode() : 0);
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
    private readonly IEnumerable<int> _anArray;

    public ProperValueType(int a, IEnumerable<int> anArray)
    {
      this._a = a;
      _anArray = anArray;
    }
  }

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
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ProperValueTypeWithoutEqualityOperator)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_a * 397) ^ (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    private readonly int _a;
    private readonly IEnumerable<int> _anArray;

    public ProperValueTypeWithoutEqualityOperator(int a, IEnumerable<int> anArray)
    {
      this._a = a;
      _anArray = anArray;
    }
  }
}
