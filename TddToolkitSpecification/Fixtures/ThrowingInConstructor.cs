using System;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public class ThrowingInConstructor
  {
    public ThrowingInConstructor()
    {
      throw new Exception();
    }
  }
}