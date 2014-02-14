using System;

namespace TddEbook.TypeReflection
{
  public class NoSuchOperatorInTypeException : Exception
  {
    public NoSuchOperatorInTypeException(string s)
      : base(s)
    {
    }
  }
}