using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommonTypes;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkitSpecification.Fixtures;
using TddEbook.TypeReflection;
using TypeReflection.ImplementationDetails;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
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
    public void ShouldPreferInternalNonRecursiveConstructorsToPublicRecursiveOnes()
    {
      Assert.DoesNotThrow(() => Any.Instance<DirectoryPath>());
      Assert.DoesNotThrow(() => XAssert.IsValue<DirectoryPath>());
    }

    [Test]
    public void ShouldBeAbleToChooseInternalConstructorWhenThereisNoPublicOne()
    {
      Assert.DoesNotThrow(() => Any.Instance<FileNameWithoutExtension>());
      Assert.DoesNotThrow(() => XAssert.IsValue<FileNameWithoutExtension>());
    }

    [Test]
    public void ShouldCorrectlyCompareCollectionsInAssertAll()
    {
      // GIVEN
      var x1 = new List<string> { "aaa", "bbb" };
      var x2 = new List<string> { "aaa", "bbb" };

      var exception = Assert.Throws<AssertionException>(
        () => XAssert.All(recorder => recorder.Equal(x1, x2))
      );

      StringAssert.Contains(
        "Expected object to be {\"aaa\", \"bbb\"}, but found {\"aaa\", \"bbb\"}", 
        exception.ToString());

      XAssert.All(assert => assert.CollectionsEqual(x1, x2));

      Assert.Throws<AssertionException>(
        () => XAssert.All(recorder => 
        recorder.CollectionsEqual(
          x1, 
          new List<string>() {"bbb", "aaa"}))
      );
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
    public void ShouldFailUpperCaseAssertionOnLowerCaseStringAndPassOnUpperCaseString()
    {
      var s = Any.String();
      Assert.Throws<AssertionException>(() => XAssert.IsUpperCase(s.ToLower()) );
      Assert.DoesNotThrow(() => XAssert.IsUpperCase(s.ToUpper()));
    }

    [Test]
    public void ShouldFailLowerCaseAssertionOnUpperCaseStringAndPassOnLowerCaseString()
    {
      var s = Any.String();
      Assert.Throws<AssertionException>(() => XAssert.IsLowerCase(s.ToUpper()));
      Assert.DoesNotThrow(() => XAssert.IsLowerCase(s.ToLower()));
    }

    [Test]
    public void ShouldFailUpperCaseAssertionOnLowerCaseCharAndPassOnUpperCaseChar()
    {
      var c = Any.AlphaChar();
      Assert.Throws<AssertionException>(() => XAssert.IsUpperCase(char.ToLower(c)));
      Assert.DoesNotThrow(() => XAssert.IsUpperCase(char.ToUpper(c)));
    }

    [Test]
    public void ShouldFailLowerCaseAssertionOnUpperCaseCharAndPassOnLowerCaseChar()
    {
      var c = Any.AlphaChar();
      Assert.Throws<AssertionException>(() => XAssert.IsLowerCase(char.ToUpper(c)));
      Assert.DoesNotThrow(() => XAssert.IsLowerCase(char.ToLower(c)));
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
          o => o.NonDecoratedMethod(0, 0)
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

    [Test]
    public void ShouldWriteFirstArgumentAsExpectedInEqualityAssertionErrorMessage()
    {
      var exception = Assert.Catch<Exception>(() => XAssert.Equal(1, 2));
      StringAssert.Contains("Expected value to be 1, but found 2", exception.ToString());
    }

    [Test]
    public void ShouldAllowToSkipSomePropertiesWhenComparingLikeness()
    {
      var tp1 = new TwoProp()
      {
        X1 = 123,
        X2 = 345,
        X3 = 999,
        x4 = 123
      };
      var tp2 = Clone.Of(tp1);
      tp2.X2 = 346;
      tp2.X3 = 346;
      tp2.x4 = 346;

      XAssert.Alike(tp2, tp1, "X2", "<X2>k__BackingField", "X3", "<X3>k__BackingField", "x4");
      XAssert.Alike(tp2, tp1, tp => tp.X2, tp => tp.X3, tp => tp.x4);

      tp1.X1 = 0;

      XAssert.NotAlike(tp2, tp1, "X2", "<X2>k__BackingField", "X3", "<X3>k__BackingField", "x4");
      XAssert.NotAlike(tp2, tp1, tp => tp.X2, tp => tp.X3, tp => tp.x4);
    }


    [Test]
    public void AllowAssertingWhetherEnumHasUniqueValues()
    {
      XAssert.EnumHasUniqueValues<EnumWithUniqueValues>();
      Assert.Throws<AssertionException>(XAssert.EnumHasUniqueValues<EnumWithRepeatingValues>);
    }

    [Test]
    public void AllowAssertingWhetherConstClassHasUniqueValues()
    {
      XAssert.HasUniqueConstants<ConstsWithUniqueValues>();

      var exception = Assert.Throws<DuplicateConstantException>(XAssert.HasUniqueConstants<ConstsWithRepeatingValues>);
      XAssert.Equal("Val1 <0> is a duplicate of Val3 <0>", exception.Message);
    }
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public enum EnumWithUniqueValues
  {
    Val1,
    Val2,
    Val3,
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public enum EnumWithRepeatingValues
  {
    Val1,
    Val2,
    Val3 = Val1,
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public class ConstsWithUniqueValues
  {
    public const int Val1 = 0;
    public const int Val2 = Val1 + 1;
    public const int Val3 = Val2+1;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public class ConstsWithRepeatingValues
  {
    public const int Val1 = 0;
    public const int Val2 = Val1 + 1;
    public const int Val3 = Val1;
  }


}





