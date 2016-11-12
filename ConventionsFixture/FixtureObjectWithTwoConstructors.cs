using System;

namespace ConventionsFixture
{
  public class FixtureObjectWithTwoConstructors
  {
    public FixtureObjectWithTwoConstructors()
    {
    }

    public FixtureObjectWithTwoConstructors(int x)
    {
    }
  }

  public class ExceptionWithTwoConstructorsException : Exception
  {
    public ExceptionWithTwoConstructorsException() : base() { }
    public ExceptionWithTwoConstructorsException(string message) : base(message) { }
  }

}
