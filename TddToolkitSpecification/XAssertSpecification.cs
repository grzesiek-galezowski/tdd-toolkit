using NUnit.Framework;
using System;
using System.Collections.Generic;
using TddEbook.TddToolkit;
using TddEbook.TypeReflection.ImplementationDetails;

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
    public void ShouldWorkForStructuresWithDefaultEquality()
    {
      XAssert.IsValue<Maybe<string>>();
    }

    [Test]
    public void ShouldWorkForPrimitves()
    {
      XAssert.IsValue<int>();
    }


    [Test]
    public void ShouldAggregateMultipleAssertionsWhenAssertionAll()
    {
      var exception = Assert.Throws<AssertionException>(() =>
        XAssert.All(assert =>
        {
          assert.Equal(1, 3);
          assert.Equal(2, 44);
          assert.Equal("aa", "123");
          assert.True(true);
          assert.Contains("bb", "aa");
        })
      );

      StringAssert.Contains("Assertion no. 1 failed: Expected object to be 1, but found 3", 
        exception.ToString());
      StringAssert.Contains("Assertion no. 2 failed: Expected object to be 2, but found 44", 
        exception.ToString());
      StringAssert.Contains("Assertion no. 3 failed: Expected object to be \"aa\", but found \"123\"", 
        exception.ToString());
      StringAssert.DoesNotContain("Assertion no. 4 failed", exception.ToString());
      StringAssert.Contains("Assertion no. 5 failed: Expected string \"bb\" to contain \"aa\"",
        exception.ToString());
    }

    [Test]
    public void ShouldThrowExceptionWhenAttributeIsNotOnMethod()
    {
      Assert.Throws<AssertionException>(() =>
        XAssert.AttributeExistsOnMethodOf<AttributeFixture>(
          new CultureAttribute("AnyCulture"), 
          o => o.NonDecoratedMethod(0,0)
        )
      );
    }

    [Test]
    public void ShouldNotThrowExceptionWhenAttributeIsOnMethod()
    {
      Assert.DoesNotThrow(() =>
        XAssert.AttributeExistsOnMethodOf<AttributeFixture>(
          new CultureAttribute("AnyCulture"),
          o => o.DecoratedMethod(0, 0)
        )
      );
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
