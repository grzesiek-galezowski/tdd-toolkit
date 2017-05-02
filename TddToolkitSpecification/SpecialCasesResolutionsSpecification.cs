using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkitSpecification.Fixtures;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkitSpecification
{
  public class SpecialCasesResolutionsSpecification
  {
    [Test]
    public void ShouldCreateResolutionCapableOfGeneratingArrays()
    {
      //GIVEN
      var resolution = SpecialCasesOfResolutions<RecursiveInterface[]>
        .CreateResolutionOfArray(new SpecialCasesOfResolutions<RecursiveInterface[]>());
      
      //WHEN

      //THEN
      Assert.True(resolution.Applies());
      XAssert.NotNull(resolution.Apply(Any.Instance<IProxyBasedGenerator>()));
      XAssert.Equal(3, resolution.Apply(Any.Instance<IProxyBasedGenerator>()).Length);

    }

  }
}
