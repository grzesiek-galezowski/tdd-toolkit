using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkitSpecification.Fixtures;
using TddEbook.TddToolkitSpecification.XAssertSpecifications;

namespace TddEbook.TddToolkitSpecification
{
  public class AnySubstituteSpecification
  {
    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndOverrideDefaultValues()
    {
      //GIVEN
      var instance = Any.SubstituteOf<RecursiveInterface>();

      //WHEN
      var result = instance.Number;

      //THEN
      XAssert.NotEqual(default(int), result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndNotOverrideStubbedValues()
    {
      //GIVEN
      var instance = Any.SubstituteOf<RecursiveInterface>();
      instance.Number.Returns(44543);

      //WHEN
      var result = instance.Number;

      //THEN
      XAssert.Equal(44543, result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndStillAllowVerifyingCalls()
    {
      //GIVEN
      var instance = Any.SubstituteOf<RecursiveInterface>();

      //WHEN
      instance.VoidMethod();

      //THEN
      instance.Received(1).VoidMethod();
    }

    [Test]
    public void ShouldReturnNonNullImplementationsOfInnerObjects()
    {
      //GIVEN
      var instance = Any.SubstituteOf<RecursiveInterface>();

      //WHEN
      var result = instance.Nested;

      //THEN
      Assert.NotNull(result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndSkipOverridingResultsStubbedWithNonDefaultValues()
    {
      var instance = Any.SubstituteOf<RecursiveInterface>();
      var anotherInstance = Substitute.For<RecursiveInterface>();
      instance.Nested.Returns(anotherInstance);

      XAssert.Equal(anotherInstance, instance.Nested);
    }

    [Test]
    public void ShouldBeAbleToBypassStaticCreationMethodWhenConstructorIsInternal()
    {
      Assert.DoesNotThrow(() => Any.Instance<FileExtension>());
      Assert.DoesNotThrow(() => Any.Instance<FileName>());
    }


    [Test]
    public void ShouldGenerateStringsContainingOtherObjects()
    {
      StringAssert.Contains("lol", Any.StringContaining("lol"));
      StringAssert.Contains("lol", Any.StringContaining<string>("lol"));
      StringAssert.Contains("2", Any.StringContaining(2));
      StringAssert.Contains("C", Any.StringContaining('C'));
    }
  }
}