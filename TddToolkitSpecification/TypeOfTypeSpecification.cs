using System;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkitSpecification
{
  public class TypeOfTypeSpecification
  {
    [Test]
    public void ShouldCorrectlyDetermineIfObjectIsOfTypeType() //this is not a typo!
    {
      XAssert.All(assert =>
      {
        assert.False(TypeOfType.Is<object>());
        assert.False(TypeOfType.Is<int>());
        assert.True(TypeOfType.Is<Type>());
      });
    }
  }
}
