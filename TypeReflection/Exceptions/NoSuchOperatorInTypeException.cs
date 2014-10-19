using System;

namespace TddEbook.TypeReflection.Exceptions
{
  public class NoSuchOperatorInTypeException : Exception
  {
    public NoSuchOperatorInTypeException(string s)
      : base(s)
    {
    }
  }
}