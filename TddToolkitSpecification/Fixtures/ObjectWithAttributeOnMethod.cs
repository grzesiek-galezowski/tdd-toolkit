using System;

namespace TddEbook.TddToolkitSpecification
{
  class AttributeFixture
  {
    [NUnit.Framework.Culture("AnyCulture")]
    public object DecoratedMethod(int p1, int p2)
    {
      throw new NotImplementedException();
    }

    public object NonDecoratedMethod(int p1, int p2)
    {
      throw new NotImplementedException();
    }

  }
}
