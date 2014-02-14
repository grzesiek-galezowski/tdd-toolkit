using System;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public class NoSuchOperatorInTypeException : Exception
  {
    public NoSuchOperatorInTypeException(string s)
      : base(s)
    {
    }
  }
}