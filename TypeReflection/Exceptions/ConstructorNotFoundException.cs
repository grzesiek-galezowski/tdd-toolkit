using System;

namespace TddEbook.TypeReflection.Exceptions
{
  public class ConstructorNotFoundException : Exception
  {
    public ConstructorNotFoundException(string s)
      : base(s)
    {
      
    }
  }
}