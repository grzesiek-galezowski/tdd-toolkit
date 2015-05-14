using NUnit.Framework;
using System;
using System.Collections.Generic;
using TddEbook.TddToolkit;

namespace TddEbook.TddToolkitSpecification
{
  public class XAssertSpecification
  {

    [Test]
    public void ShouldThrowAssertionExceptionWhenTypeIsNotGuardedAgainstNullConstructorParameters()
    {
      var exception = Assert.Throws<AssertionException>(XAssert.HasNullProtectedConstructors<NotGuardedObject>);
      StringAssert.Contains("Not guarded parameter: String b", exception.Message);
      StringAssert.Contains("Not guarded parameter: String dede", exception.Message);
      StringAssert.DoesNotContain("Not guarded parameter: Int32 a", exception.Message);
    }

    [Test]
    public void ShouldNotThrowAssertionExceptionWhenTypeIsGuardedAgainstNullConstructorParameters()
    {
      Assert.DoesNotThrow(XAssert.HasNullProtectedConstructors<GuardedObject>);
    }

    [Test]
    public void ShouldPassValueTypeAssertionForProperValueType()
    {
      XAssert.IsValue<ProperValueType>();
    }

    [Test]
    public void ShouldPassValueTypeAssertionForProperValueTypeWithInternalConstructor()
    {
      XAssert.IsValue<FileExtension>();
    }

    [Test]
    public void ShouldAllowSpecifyingConstructorArgumentsNotTakenIntoAccountDuringValueBehaviorCheck()
    {
      XAssert.IsValue<ProperValueTypeWithOneArgumentIdentity>(
        ValueTypeTraits.Custom.SkipConstructorArgument(0));

      Assert.Throws<AssertionException>(XAssert.IsValue<ProperValueTypeWithOneArgumentIdentity>);
    }

    [Test]
    public void ShouldAcceptProperFullValueTypesAndRejectBadOnes()
    {
      XAssert.IsValue<ProperValueType>();

      Assert.Throws<AssertionException>(XAssert.IsValue<ProperValueTypeWithoutEqualityOperator>);
      
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
      if (obj.GetType() != GetType()) return false;
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
      if(ReferenceEquals(left, null) && ReferenceEquals(right, null))
      {
        return true;
      }
      else if(ReferenceEquals(left, null))
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
      _a = a;
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
      if (obj.GetType() != GetType()) return false;
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
      _a = a;
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
      if (obj.GetType() != GetType()) return false;
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
      _a = a;
      _anArray = anArray;
    }
  }


  public class NotGuardedObject
  {
    public NotGuardedObject(int a, string b, int c, string dede)
    {
      
    }

  }

  public class GuardedObject
  {
    public GuardedObject(int a, string b, int c, string dede)
    {
      Guard.ArgumentNotNull(b, "x");
      Guard.ArgumentNotNull(dede, "y");
    }
  }


  public class FileExtension
    : IEquatable<FileExtension>
  {
    private readonly string _extension;

    internal FileExtension(string extension)
    {
      this._extension = extension;
    }

    public override string ToString()
    {
      return _extension;
    }

    public bool Equals(FileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_extension, other._extension);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileExtension)obj);
    }

    public override int GetHashCode()
    {
      return (_extension != null ? _extension.GetHashCode() : 0);
    }

    public static bool operator ==(FileExtension left, FileExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileExtension left, FileExtension right)
    {
      return !Equals(left, right);
    }

  }


}
