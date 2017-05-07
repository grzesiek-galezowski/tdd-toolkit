using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.Subgenerators;
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
      var resolution = new SpecialCasesOfResolutions<RecursiveInterface[]>(new CollectionGenerator(new GenericMethodProxyCalls())).CreateResolutionOfArray();
      
      //WHEN

      //THEN
      Assert.True(resolution.Applies());
      XAssert.NotNull(resolution.Apply(Any.Instance<IInstanceGenerator>()));
      XAssert.Equal(3, resolution.Apply(Any.Instance<IInstanceGenerator>()).Length);

    }

  }
}
